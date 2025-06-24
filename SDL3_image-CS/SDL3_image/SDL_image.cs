// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static unsafe partial class SDL3_image
    {
        [Constant]
        public static readonly int SDL_IMAGE_VERSION = SDL3.SDL_VERSIONNUM(SDL_IMAGE_MAJOR_VERSION, SDL_IMAGE_MINOR_VERSION, SDL_IMAGE_MICRO_VERSION);

        [Macro]
        public static bool SDL_IMAGE_VERSION_ATLEAST(int X, int Y, int Z) =>
            ((SDL_IMAGE_MAJOR_VERSION >= X) &&
             (SDL_IMAGE_MAJOR_VERSION > X || SDL_IMAGE_MINOR_VERSION >= Y) &&
             (SDL_IMAGE_MAJOR_VERSION > X || SDL_IMAGE_MINOR_VERSION > Y || SDL_IMAGE_MICRO_VERSION >= Z));
    }
}
