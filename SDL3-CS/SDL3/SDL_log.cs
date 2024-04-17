// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        public static void SDL_LogSetPriority(SDL_LogCategory category, SDL_LogPriority priority) => SDL_LogSetPriority((int)category, priority);
        public static SDL_LogPriority SDL_LogGetPriority(SDL_LogCategory category) => SDL_LogGetPriority((int)category);
    }
}
