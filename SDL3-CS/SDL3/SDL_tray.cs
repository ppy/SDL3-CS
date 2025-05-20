// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_TrayEntryFlags : UInt32
    {
        SDL_TRAYENTRY_BUTTON = SDL3.SDL_TRAYENTRY_BUTTON,
        SDL_TRAYENTRY_CHECKBOX = SDL3.SDL_TRAYENTRY_CHECKBOX,
        SDL_TRAYENTRY_SUBMENU = SDL3.SDL_TRAYENTRY_SUBMENU,
        SDL_TRAYENTRY_DISABLED = SDL3.SDL_TRAYENTRY_DISABLED,
        SDL_TRAYENTRY_CHECKED = SDL3.SDL_TRAYENTRY_CHECKED,
    }

    public static partial class SDL3
    {
        public static unsafe SDLConstOpaquePointerArray<SDL_TrayEntry>? SDL_GetTrayEntries(SDL_TrayMenu* menu)
        {
            int count;
            var array = SDL_GetTrayEntries(menu, &count);
            return SDLArray.CreateConstOpaque(array, count);
        }
    }
}
