// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public unsafe partial class SDL3
    {
        [Macro]
        public static int SDL_AtomicIncRef(SDL_AtomicInt* a) => SDL_AtomicAdd(a, 1);

        [Macro]
        public static bool SDL_AtomicDecRef(SDL_AtomicInt* a) => SDL_AtomicAdd(a, -1) == 1;
    }
}
