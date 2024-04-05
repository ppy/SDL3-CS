# Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
# See the LICENCE file in the repository root for full licence text.

"""
Generates C# bindings for SDL3 using ClangSharp.

Prerequisites:
- run `dotnet tool restore` (to install ClangSharpPInvokeGenerator)
- https://github.com/libsdl-org/SDL checked out alongside this repository

This script should be run manually.
"""

import pathlib
import subprocess

SDL_root = pathlib.Path("../../SDL")
SDL_include_root = SDL_root / "include"
SDL3_header_base = "SDL3"  # base folder of header files

csproj_root = pathlib.Path(".")


class Header:
    """Represents a SDL header file that is used in ClangSharp generation."""

    def __init__(self, base: str, name: str):
        assert base == SDL3_header_base
        assert name.startswith("SDL")
        assert not name.endswith(".h")
        self.base = base
        self.name = name

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
        return csproj_root / f"{self.base}/ClangSharp/{self.name}.g.cs"


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
    add("SDL3/SDL_main.h"),
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
    add("SDL3/SDL_quit.h"),
    add("SDL3/SDL_rect.h"),
    add("SDL3/SDL_render.h"),
    add("SDL3/SDL_revision.h"),
    add("SDL3/SDL_scancode.h"),
    add("SDL3/SDL_sensor.h"),
    add("SDL3/SDL_stdinc.h"),
    add("SDL3/SDL_storage.h"),
    add("SDL3/SDL_surface.h"),
    add("SDL3/SDL_system.h"),
    add("SDL3/SDL_thread.h"),
    add("SDL3/SDL_time.h"),
    add("SDL3/SDL_timer.h"),
    add("SDL3/SDL_touch.h"),
    add("SDL3/SDL_version.h"),
    add("SDL3/SDL_video.h"),
    add("SDL3/SDL_vulkan.h"),
]

base_command = [
    "dotnet", "tool", "run", "ClangSharpPInvokeGenerator",
    "--headerFile", csproj_root / "SDL.licenseheader",

    "--config",
    "latest-codegen",
    "windows-types",
    "generate-macro-bindings",
    "log-potential-typedef-remappings",

    "--file-directory", SDL_include_root,
    "--include-directory", SDL_include_root,
    "--libraryPath", "SDL3",
    "--methodClassName", "SDL3",
    "--namespace", "SDL",
]


def run_clangsharp(command, header: Header):
    cmd = command + [
        "--file", header.input_file(),
        "--output", header.output_file(),
    ]

    subprocess.run(cmd)


def main():
    for header in headers:
        run_clangsharp(base_command, header)


if __name__ == "__main__":
    main()
