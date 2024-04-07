// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static unsafe bool SDL_MUSTLOCK(SDL_Surface* S) => (((S)->flags & SDL_RLEACCEL) != 0);
    }
}
