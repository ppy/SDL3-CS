// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace SDL
{
    public static partial class SDL3
    {
        /// <returns>
        /// An array of <see cref="nint"/> that can be passed to <see cref="Marshal.PtrToStringUTF8(System.IntPtr)"/>.
        /// </returns>
        public static unsafe SDLArray<IntPtr>? SDL_GetGamepadMappings()
        {
            int count;
            IntPtr* array = (IntPtr*)SDL_GetGamepadMappings(&count);
            return SDLArray.CreatePooled(array, count);
        }

        public static unsafe SDLArray<SDL_JoystickID>? SDL_GetGamepads()
        {
            int count;
            var array = SDL_GetGamepads(&count);
            return SDLArray.CreatePooled(array, count);
        }

        public static unsafe SDLPointerArray<SDL_GamepadBinding>? SDL_GetGamepadBindings(SDL_Gamepad* gamepad)
        {
            int count;
            var array = SDL_GetGamepadBindings(gamepad, &count);
            return SDLArray.CreatePooled(array, count);
        }
    }
}
