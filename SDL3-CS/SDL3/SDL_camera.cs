// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_CameraDeviceID : UInt32;

    public static partial class SDL3
    {
        public static unsafe SDLArray<SDL_CameraDeviceID>? SDL_GetCameraDevices()
        {
            int count;
            var array = SDL_GetCameraDevices(&count);
            return SDLArray.Create(array, count);
        }

        public static unsafe SDLArray<SDL_CameraSpec>? SDL_GetCameraDeviceSupportedFormats(SDL_CameraDeviceID devid)
        {
            int count;
            var array = SDL_GetCameraDeviceSupportedFormats(devid, &count);
            return SDLArray.Create(array, count);
        }
    }
}
