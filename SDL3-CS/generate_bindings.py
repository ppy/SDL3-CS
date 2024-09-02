# Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
# See the LICENCE file in the repository root for full licence text.

"""
Generates C# bindings for SDL3 using ClangSharp.

Prerequisites:
- run `dotnet tool restore` (to install ClangSharpPInvokeGenerator)
- https://github.com/libsdl-org/SDL checked out alongside this repository
- git apply --3way `SDL-use-proper-types.patch` to SDL repo

This script should be run manually.
"""

import json
import pathlib
import re
import subprocess
import sys

# Needs to match SDL3.SourceGeneration.Helper.UnsafePrefix
unsafe_prefix = "Unsafe_"

SDL_root = pathlib.Path("../../SDL")
SDL_include_root = SDL_root / "include"
SDL3_header_base = "SDL3"  # base folder of header files

csproj_root = pathlib.Path(".")


class Header:
    """Represents a SDL header file that is used in ClangSharp generation."""

    def __init__(self, base: str, name: str, output_suffix=None):
        assert base == SDL3_header_base
        assert name.startswith("SDL")
        assert not name.endswith(".h")
        self.base = base
        self.name = name
        self.output_suffix = output_suffix

    def __str__(self):
        return self.input_file()

    def sdl_api_name(self):
        """Header name in sdl.json API dump."""
        return f"{self.name}.h"

    def input_file(self):
        """Input header file relative to SDL_include_root."""
        return f"{self.base}/{self.name}.h"

    def output_file(self):
        """Location of generated C# file."""
        if self.output_suffix is None:
            return csproj_root / f"{self.base}/ClangSharp/{self.name}.g.cs"
        else:
            return csproj_root / f"{self.base}/ClangSharp/{self.name}.{self.output_suffix}.g.cs"

    def rsp_files(self):
        """Location of ClangSharp response files."""
        yield csproj_root / f"{self.base}/{self.name}.rsp"
        if self.output_suffix is not None:
            yield csproj_root / f"{self.base}/{self.name}.{self.output_suffix}.rsp"

    def cs_file(self):
        """Location of the manually-written C# file that implements some parts of the header."""
        if self.output_suffix is None:
            return csproj_root / f"{self.base}/{self.name}.cs"
        else:
            return csproj_root / f"{self.base}/{self.name}.{self.output_suffix}.cs"


def add(s: str):
    base, name = s.split("/")
    assert s.endswith(".h")
    name = name.replace(".h", "")
    return Header(base, name)


headers = [
    add("SDL3/SDL_atomic.h"),
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
    add("SDL3/SDL_touch.h"),
    add("SDL3/SDL_version.h"),
    add("SDL3/SDL_video.h"),
    add("SDL3/SDL_vulkan.h"),
]


def get_sdl_api_dump():
    subprocess.run([
        sys.executable,
        SDL_root / "src" / "dynapi" / "gendynapi.py",
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


defined_constant_regex = re.compile(r"\[Constant]\s*public (const|static readonly) \w+ (SDL_\w+) = ", re.MULTILINE)


def get_manually_written_symbols(header):
    """Returns symbols names whose definitions are manually written in C#."""
    cs_file = header.cs_file()
    if cs_file.is_file():
        with open(cs_file, "r", encoding="utf-8") as f:
            text = f.read()
            for match in defined_constant_regex.finditer(text):
                m = match.group(2)
                assert m.startswith("SDL_")
                yield m


typedef_enum_regex = re.compile(r"\[Typedef]\s*public enum (SDL_\w+)", re.MULTILINE)


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
    "--headerFile", csproj_root / "SDL.licenseheader",

    "--config",
    "latest-codegen",
    "windows-types",
    "generate-macro-bindings",

    "--file-directory", SDL_include_root,
    "--include-directory", SDL_include_root,
    "--libraryPath", "SDL3",
    "--methodClassName", "SDL3",
    "--namespace", "SDL",

    "--remap",
    "void*=IntPtr",
    "char=byte",
    "wchar_t *=IntPtr",  # wchar_t has a platform-defined size

    "--define-macro",
    "SDL_FUNCTION_POINTER_IS_VOID_POINTER",

    "--additional",
    "--undefine-macro=_WIN32",
]


def run_clangsharp(command, header: Header):
    cmd = command + [
        "--file", header.input_file(),
        "--output", header.output_file(),
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


# regex for ClangSharp-generated SDL functions
generated_function_regex = re.compile(r"public static extern \w+\** (SDL_\w+)\(")


def get_generated_functions(file):
    with open(file, "r", encoding="utf-8") as f:
        for match in generated_function_regex.finditer(f.read()):
            yield match.group(1)


def generate_platform_specific_headers(sdl_api, header: Header, platforms):
    all_functions = list(all_funcs_from_header(sdl_api, header))

    print(f"💠 {header} platform agnostic")
    platform_agnostic_cs = run_clangsharp(base_command, header)
    platform_agnostic_functions = list(get_generated_functions(platform_agnostic_cs))
    output_files = [platform_agnostic_cs]

    for (defines, suffix, platform_name) in platforms:
        command = base_command + ["--define-macro"] + defines

        if platform_agnostic_functions:
            command.append("--exclude")
            command.extend(platform_agnostic_functions)

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
            yield f


def main():
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
        for func in str_ret_funcs:
            name = func["name"]
            # add unsafe prefix to `const char *` functions so that the source generator can make friendly overloads with the unprefixed name.
            base_command.append(f"{name}={unsafe_prefix}{name}")

    for header in headers:
        output_file = run_clangsharp(base_command, header)
        check_generated_functions(sdl_api, header, [output_file])

    generate_platform_specific_headers(sdl_api, add("SDL3/SDL_main.h"), [
        (["SDL_PLATFORM_WIN32", "SDL_PLATFORM_WINGDK"], "Windows", "Windows"),
    ])

    generate_platform_specific_headers(sdl_api, add("SDL3/SDL_system.h"), [
        # define macro, output_suffix, [SupportedOSPlatform]
        (["SDL_PLATFORM_ANDROID"], "Android", "Android"),
        (["SDL_PLATFORM_IOS"], "iOS", "iOS"),
        (["SDL_PLATFORM_LINUX"], "Linux", "Linux"),
        (["SDL_PLATFORM_WIN32", "SDL_PLATFORM_WINGDK"], "Windows", "Windows"),
        (["SDL_PLATFORM_WINRT"], "WinRT", "Windows"),
    ])


if __name__ == "__main__":
    main()
