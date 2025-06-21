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

#pragma warning disable CS0618 // Type or member is obsolete
    public partial struct SDL_VirtualJoystickDesc : SDL3.ISDLInterface
#pragma warning restore CS0618 // Type or member is obsolete
    {
        uint SDL3.ISDLInterface.version { set => version = value; }
    }
}
