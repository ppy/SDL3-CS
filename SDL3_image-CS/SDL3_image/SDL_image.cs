﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum IMG_InitFlags : int
    {
        IMG_INIT_JPG = SDL3_image.IMG_INIT_JPG,
        IMG_INIT_PNG = SDL3_image.IMG_INIT_PNG,
        IMG_INIT_TIF = SDL3_image.IMG_INIT_TIF,
        IMG_INIT_WEBP = SDL3_image.IMG_INIT_WEBP,
        IMG_INIT_JXL = SDL3_image.IMG_INIT_JXL,
        IMG_INIT_AVIF = SDL3_image.IMG_INIT_AVIF,
    }

    public static unsafe partial class SDL3_image
    {
        [Constant]
        public static readonly int SDL_IMAGE_VERSION = SDL3.SDL_VERSIONNUM(SDL_IMAGE_MAJOR_VERSION, SDL_IMAGE_MINOR_VERSION, SDL_IMAGE_MICRO_VERSION);

#pragma warning disable CA2255
        [ModuleInitializer]
        internal static void ModuleInitializer()
        {
            SDL3.SDL_Init(0);
        }
#pragma warning restore CA2255
    }
}
