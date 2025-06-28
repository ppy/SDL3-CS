# Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
# See the LICENCE file in the repository root for full licence text.

"""
Generates C# bindings for SDL3 using ClangSharp.

Prerequisites:
- run `dotnet tool restore` (to install ClangSharpPInvokeGenerator)

This script should be run manually.

Usage:
- Run the script without any command line parameters to generate all header files.
- Provide header names as command line parameters to generate just those headers.

Example:
- python generate_bindings.py
- python generate_bindings.py SDL3/SDL_audio.h
- python generate_bindings.py SDL3_ttf/SDL_ttf.h
- python generate_bindings.py SDL_audio.h
- python generate_bindings.py SDL_audio
- python generate_bindings.py audio
- python generate_bindings.py SDL_camera.h SDL_init.h
"""

import json
import pathlib
import platform
import re
import subprocess
import sys

# Needs to match SDL3.SourceGeneration.Helper.UnsafePrefix
unsafe_prefix = "Unsafe_"

repository_root = pathlib.Path(__file__).resolve().parents[1]

SDL_lib_root = "External"
SDL_libs = ["SDL", "SDL_image", "SDL_ttf", "SDL_mixer"]
SDL_lib_include_root = {
    "SDL3": SDL_lib_root + "/SDL/include",
    "SDL3_image": SDL_lib_root + "/SDL_image/include",
    "SDL3_ttf": SDL_lib_root + "/SDL_ttf/include",
    "SDL3_mixer": SDL_lib_root + "/SDL_mixer/include",
}

SDL3_header_base = "SDL3"  # base folder of header files

csproj_root = repository_root / "SDL3-CS"


class Header:
    """Represents a SDL header file that is used in ClangSharp generation."""

    def __init__(self, base: str, name: str, folder: str, output_suffix=None):
        assert base in SDL_lib_include_root
        assert name.startswith("SDL")
        assert not name.endswith(".h")
        self.base = base
        self.name = name
        self.folder = folder
        self.output_suffix = output_suffix

    def __str__(self):
        return self.input_file()

    def sdl_api_name(self):
        """Header name in sdl.json API dump."""
        return f"{self.name}.h"

    def input_file(self):
        """Input header file relative to repository_root."""
        return f"{self.folder}/{self.base}/{self.name}.h"

    def output_file(self):
        """Location of generated C# file."""
        if self.output_suffix is None:
            return repository_root / f"{self.base}-CS" / f"{self.base}/ClangSharp/{self.name}.g.cs"
        else:
            return repository_root / f"{self.base}-CS" / f"{self.base}/ClangSharp/{self.name}.{self.output_suffix}.g.cs"

    def rsp_files(self):
        """Location of ClangSharp response files."""
        yield repository_root / f"{self.base}-CS" / f"{self.base}/{self.name}.rsp"
        if self.output_suffix is not None:
            yield repository_root / f"{self.base}-CS" / f"{self.base}/{self.name}.{self.output_suffix}.rsp"

    def cs_file(self):
        """Location of the manually-written C# file that implements some parts of the header."""
        if self.output_suffix is None:
            return repository_root / f"{self.base}-CS" / f"{self.base}/{self.name}.cs"
        else:
            return repository_root / f"{self.base}-CS" / f"{self.base}/{self.name}.{self.output_suffix}.cs"


def make_header_fuzzy(s: str) -> Header:
    match s.split("/"):
        case [name]:  # one part, eg "SDL_audio.h" or "audio"
            base = "SDL3"  # assume a default base
        case [base, name]:  # two parts, eg "SDL3/SDL_audio.h"
            pass
        case _:
            raise ValueError(f"Can't match \"{s}\" to header name.")

    if not name.startswith("SDL_"):
        name = f'SDL_{name}'

    if name.endswith(".h"):
        name = name.replace(".h", "")

    return Header(base, name, SDL_lib_include_root[base])


def add(s: str):
    base, name = s.split("/")
    assert s.endswith(".h")
    name = name.replace(".h", "")
    return Header(base, name, SDL_lib_include_root[base])


headers = [
    add("SDL3/SDL_atomic.h"),
    add("SDL3/SDL_asyncio.h"),
    add("SDL3/SDL_audio.h"),
    add("SDL3/SDL_blendmode.h"),
    add("SDL3/SDL_camera.h"),
    add("SDL3/SDL_clipboard.h"),
    add("SDL3/SDL_cpuinfo.h"),
    add("SDL3/SDL_dialog.h"),
    add("SDL3/SDL_error.h"),
    add("SDL3/SDL_events.h"),
    add("SDL3/SDL_filesystem.h"),
    add("SDL3/SDL_gamepad.h"),
    add("SDL3/SDL_gpu.h"),
    add("SDL3/SDL_guid.h"),
    add("SDL3/SDL_haptic.h"),
    add("SDL3/SDL_hidapi.h"),
    add("SDL3/SDL_hints.h"),
    add("SDL3/SDL_init.h"),
    add("SDL3/SDL_iostream.h"),
    add("SDL3/SDL_joystick.h"),
    add("SDL3/SDL_keyboard.h"),
    add("SDL3/SDL_keycode.h"),
    add("SDL3/SDL_loadso.h"),
    add("SDL3/SDL_locale.h"),
    add("SDL3/SDL_log.h"),
    add("SDL3/SDL_messagebox.h"),
    add("SDL3/SDL_metal.h"),
    add("SDL3/SDL_misc.h"),
    add("SDL3/SDL_mouse.h"),
    add("SDL3/SDL_mutex.h"),
    add("SDL3/SDL_pen.h"),
    add("SDL3/SDL_pixels.h"),
    add("SDL3/SDL_platform.h"),
    add("SDL3/SDL_power.h"),
    add("SDL3/SDL_process.h"),
    add("SDL3/SDL_properties.h"),
    add("SDL3/SDL_rect.h"),
    add("SDL3/SDL_render.h"),
    add("SDL3/SDL_revision.h"),
    add("SDL3/SDL_scancode.h"),
    add("SDL3/SDL_sensor.h"),
    add("SDL3/SDL_stdinc.h"),
    add("SDL3/SDL_storage.h"),
    add("SDL3/SDL_surface.h"),
    add("SDL3/SDL_thread.h"),
    add("SDL3/SDL_time.h"),
    add("SDL3/SDL_timer.h"),
    add("SDL3/SDL_tray.h"),
    add("SDL3/SDL_touch.h"),
    add("SDL3/SDL_version.h"),
    add("SDL3/SDL_video.h"),
    add("SDL3/SDL_vulkan.h"),
    add("SDL3_image/SDL_image.h"),
    add("SDL3_ttf/SDL_ttf.h"),
    add("SDL3_ttf/SDL_textengine.h"),
    add("SDL3_mixer/SDL_mixer.h"),
]


def prepare_sdl_source():
    for lib in SDL_libs:
        subprocess.run([
            "git",
            "reset",
            "--hard",
            "HEAD"
        ], cwd=repository_root / SDL_lib_root / lib)


def get_sdl_api_dump():
    subprocess.run([
        sys.executable,
        repository_root / SDL_lib_root / "SDL" / "src" / "dynapi" / "gendynapi.py",
        "--dump"
    ])

    with open("sdl.json", "r", encoding="utf-8") as f:
        return json.load(f)


def all_funcs_from_header(sdl_api, header):
    for f in sdl_api:
        if f["header"] == header.sdl_api_name():
            yield f


def get_text(file_paths):
    text = ""
    for path in file_paths:
        with open(path, "r", encoding="utf-8") as f:
            text += f.read()

    return text


def check_generated_functions(sdl_api, header, generated_file_paths):
    """Checks that the generated C# files contain the expected function definitions."""
    all_files_text = get_text(generated_file_paths)

    for func in all_funcs_from_header(sdl_api, header):
        name = func["name"]
        found = f"{name}(" in all_files_text

        if not found:
            print(f"[⚠️ Warning] Function {name} not found in generated files:", *generated_file_paths)


defined_constant_regex = re.compile(r"\[Constant]\s*public (const|static readonly) \w+ (\w+_\w+) = ", re.MULTILINE)


def get_manually_written_symbols(header):
    """Returns symbols names whose definitions are manually written in C#."""
    cs_file = header.cs_file()
    if cs_file.is_file():
        with open(cs_file, "r", encoding="utf-8") as f:
            text = f.read()
            for match in defined_constant_regex.finditer(text):
                m = match.group(2)
                yield m


typedef_enum_regex = re.compile(r"\[Typedef]\s*public enum (\w+_\w+)", re.MULTILINE)


def get_typedefs():
    for header in headers:
        cs_file = header.cs_file()
        if cs_file.is_file():
            with open(cs_file, "r", encoding="utf-8") as f:
                for match in typedef_enum_regex.finditer(f.read()):
                    yield match.group(1)


def typedef(t):
    return f"{t}={t}"


base_command = [
    "dotnet", "tool", "run", "ClangSharpPInvokeGenerator",
    "--headerFile", csproj_root / "SDL-license-header.txt",

    "--config",
    "latest-codegen",
    "windows-types",
    "generate-macro-bindings",

    "--file-directory", repository_root,
    "--include-directory", repository_root / SDL_lib_include_root["SDL3"],
    "--include-directory", repository_root / SDL_lib_include_root["SDL3_image"],
    "--include-directory", repository_root / SDL_lib_include_root["SDL3_ttf"],
    "--include-directory", repository_root / SDL_lib_include_root["SDL3_mixer"],
    "--namespace", "SDL",

    "--remap",
    "void*=IntPtr",
    "char=byte",
    "wchar_t *=IntPtr",  # wchar_t has a platform-defined size
    "bool=SDLBool",  # treat bool as C# helper type
    "__va_list=byte*",
    "__va_list_tag=byte",
    "Sint64=long",
    "Uint64=ulong",

    "--with-type",
    "*=int",  # all enum types should be ints by default

    "--nativeTypeNamesToStrip",
    "unsigned int",

    "--define-macro",
    "SDL_FUNCTION_POINTER_IS_VOID_POINTER",
    "SDL_SINT64_C(c)=c ## LL",
    "SDL_UINT64_C(c)=c ## ULL",
    "SDL_DECLSPEC=",  # Not supported by llvm

    # Undefine platform-specific macros - these will be defined on a per-case basis later.
    "--additional",
    "--undefine-macro=_WIN32",
    "--undefine-macro=linux",
    "--undefine-macro=__linux",
    "--undefine-macro=__linux__",
    "--undefine-macro=unix",
    "--undefine-macro=__unix",
    "--undefine-macro=__unix__",
    "--undefine-macro=__APPLE__",
]


def run_clangsharp(command, header: Header):
    cmd = command + [
        "--file", header.input_file(),
        "--output", header.output_file(),
        "--libraryPath", header.base,
        
        "--methodClassName", header.base,
    ]

    for rsp in header.rsp_files():
        if rsp.is_file():
            cmd.append(f"@{rsp}")

    to_exclude = list(get_manually_written_symbols(header))
    if to_exclude:
        cmd.append("--exclude")
        cmd.extend(to_exclude)

    subprocess.run(cmd)
    return header.output_file()


# regex for ClangSharp-generated SDL functions and enums
generated_symbol_regex = re.compile(r"public (enum|static extern \w+\**) (SDL_\w+)")


def get_generated_symbols(file):
    with open(file, "r", encoding="utf-8") as f:
        for match in generated_symbol_regex.finditer(f.read()):
            yield match.group(2)


def generate_platform_specific_headers(sdl_api, header: Header, platforms):
    all_functions = list(all_funcs_from_header(sdl_api, header))

    print(f"💠 {header} platform agnostic")
    platform_agnostic_cs = run_clangsharp(base_command, header)
    platform_agnostic_symbols = list(get_generated_symbols(platform_agnostic_cs))
    output_files = [platform_agnostic_cs]

    for (defines, suffix, platform_name) in platforms:
        command = base_command + ["--define-macro"] + defines

        if platform_agnostic_symbols:
            command.append("--exclude")
            command.extend(platform_agnostic_symbols)

        if all_functions:
            command.append("--with-attribute")
            for f in all_functions:
                command.append(f'{f["name"]}=SupportedOSPlatform("{platform_name}")')

        print(f"💠 {header} for {suffix}")
        header.output_suffix = suffix
        output_files.append(run_clangsharp(command, header))

    check_generated_functions(sdl_api, header, output_files)


def get_string_returning_functions(sdl_api):
    for f in sdl_api:
        if f["retval"] in ("const char*", "char*"):
            yield f["name"]

    yield "TTF_GetFontFamilyName"
    yield "TTF_GetFontStyleName"


def should_skip(solo_headers: list[Header], header: Header):
    if len(solo_headers) == 0:
        return False

    return not any(header.input_file() == h.input_file() for h in solo_headers)


def main():
    solo_headers = [make_header_fuzzy(header_name) for header_name in sys.argv[1:]]

    if platform.system() != "Windows":
        base_command.extend([
            "--include-directory", csproj_root / "include"
        ])

    prepare_sdl_source()

    sdl_api = get_sdl_api_dump()

    # typedefs are added globally as their types appear outside of the defining header
    typedefs = list(get_typedefs())
    if typedefs:
        base_command.append("--remap")
        for type_name in typedefs:
            base_command.append(typedef(type_name))

    str_ret_funcs = list(get_string_returning_functions(sdl_api))
    if str_ret_funcs:
        base_command.append("--remap")
        for name in str_ret_funcs:
            # add unsafe prefix to `const char *` functions so that the source generator can make friendly overloads with the unprefixed name.
            base_command.append(f"{name}={unsafe_prefix}{name}")

    for header in headers:
        if should_skip(solo_headers, header):
            continue

        output_file = run_clangsharp(base_command, header)
        check_generated_functions(sdl_api, header, [output_file])

    main_header = add("SDL3/SDL_main.h")
    if not should_skip(solo_headers, main_header):
        generate_platform_specific_headers(sdl_api, main_header, [
            (["SDL_PLATFORM_WINDOWS"], "Windows", "Windows"),
        ])

    system_header = add("SDL3/SDL_system.h")
    if not should_skip(solo_headers, system_header):
        generate_platform_specific_headers(sdl_api, system_header, [
            # define macro, output_suffix, [SupportedOSPlatform]
            (["SDL_PLATFORM_ANDROID"], "Android", "Android"),
            (["SDL_PLATFORM_IOS"], "iOS", "iOS"),
            (["SDL_PLATFORM_LINUX"], "Linux", "Linux"),
            (["SDL_PLATFORM_WINDOWS", "SDL_PLATFORM_WIN32"], "Windows", "Windows"),
            (["SDL_PLATFORM_GDK"], "GDK", "Windows"),
        ])


if __name__ == "__main__":
    main()
