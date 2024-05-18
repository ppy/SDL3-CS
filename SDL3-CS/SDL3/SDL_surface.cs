// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_SurfaceFlags : UInt32
    {
        SDL_PREALLOC = SDL3.SDL_PREALLOC,
        SDL_RLEACCEL = SDL3.SDL_RLEACCEL,
        SDL_DONTFREE = SDL3.SDL_DONTFREE,
        SDL_SIMD_ALIGNED = SDL3.SDL_SIMD_ALIGNED,
        SDL_SURFACE_USES_PROPERTIES = SDL3.SDL_SURFACE_USES_PROPERTIES,
    }

    public static partial class SDL3
    {
        [Macro]
        public static unsafe bool SDL_MUSTLOCK(SDL_Surface* S) => (((S)->flags & SDL_SurfaceFlags.SDL_RLEACCEL) != 0);
    }
}
