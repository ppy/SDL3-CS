// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        public static int SDL_Init(SDL_InitFlags flags) => SDL_Init((uint)flags);

        public static int SDL_InitSubSystem(SDL_InitFlags flags) => SDL_InitSubSystem((uint)flags);

        public static void SDL_QuitSubSystem(SDL_InitFlags flags) => SDL_QuitSubSystem((uint)flags);

        public static SDL_InitFlags SDL_WasInit(SDL_InitFlags flags) => (SDL_InitFlags)SDL_WasInit((uint)flags);
    }
}
