// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_JoystickID : UInt32;

    public static partial class SDL3
    {
        [MustDisposeResource]
        public static unsafe SDLArray<SDL_JoystickID>? SDL_GetJoysticks()
        {
            int count;
            var array = SDL_GetJoysticks(&count);
            return SDLArray.Create(array, count);
        }
    }
}
