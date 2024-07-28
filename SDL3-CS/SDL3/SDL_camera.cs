// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_CameraID : UInt32;

    public static partial class SDL3
    {
        [MustDisposeResource]
        public static unsafe SDLArray<SDL_CameraID>? SDL_GetCameras()
        {
            int count;
            var array = SDL_GetCameras(&count);
            return SDLArray.Create(array, count);
        }

        [MustDisposeResource]
        public static unsafe SDLPointerArray<SDL_CameraSpec>? SDL_GetCameraSupportedFormats(SDL_CameraID devid)
        {
            int count;
            var array = SDL_GetCameraSupportedFormats(devid, &count);
            return SDLArray.Create(array, count);
        }
    }
}
