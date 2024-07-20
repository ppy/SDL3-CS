// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_HapticID : UInt32;

    public static partial class SDL3
    {
        public static unsafe SDLArray<SDL_HapticID>? SDL_GetHaptics()
        {
            int count;
            var array = SDL_GetHaptics(&count);
            return SDLArray.CreatePooled(array, count);
        }
    }
}
