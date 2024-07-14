// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_SurfaceFlags : UInt32
    {
        SDL_SURFACE_PREALLOCATED = SDL3.SDL_SURFACE_PREALLOCATED,
        SDL_SURFACE_LOCK_NEEDED = SDL3.SDL_SURFACE_LOCK_NEEDED,
        SDL_SURFACE_LOCKED = SDL3.SDL_SURFACE_LOCKED,
        SDL_SURFACE_SIMD_ALIGNED = SDL3.SDL_SURFACE_SIMD_ALIGNED
    }

    public static partial class SDL3
    {
        [Macro]
        public static unsafe bool SDL_MUSTLOCK(SDL_Surface* S) => (S->flags & (SDL_SurfaceFlags.SDL_SURFACE_LOCK_NEEDED | SDL_SurfaceFlags.SDL_SURFACE_LOCKED)) == SDL_SurfaceFlags.SDL_SURFACE_LOCK_NEEDED;
    }
}
