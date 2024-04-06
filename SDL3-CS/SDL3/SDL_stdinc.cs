// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_bool
    {
        SDL_FALSE = SDL3.SDL_FALSE,
        SDL_TRUE = SDL3.SDL_TRUE
    }

    [Typedef]
    public enum SDL_Time : Int64;

    public partial class SDL3
    {
        [Macro]
        public static uint SDL_FOURCC(byte A, byte B, byte C, byte D) => (uint)((A << 0) | (B << 8) | (C << 16) | (D << 24));

        public static unsafe void SDL_free(void* mem) => SDL_free((IntPtr)mem);
    }
}
