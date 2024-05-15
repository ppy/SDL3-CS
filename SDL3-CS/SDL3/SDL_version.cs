// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static int SDL_VERSIONNUM(int major, int minor, int patch) =>
            ((major) * 1000000 + (minor) * 1000 + (patch));

        [Macro]
        public static int SDL_VERSIONNUM_MAJOR(int version) => ((version) / 1000000);

        [Macro]
        public static int SDL_VERSIONNUM_MINOR(int version) => (((version) / 1000) % 1000);

        [Macro]
        public static int SDL_VERSIONNUM_MICRO(int version) => ((version) % 1000);

        [Constant]
        public static readonly int SDL_VERSION = SDL_VERSIONNUM(SDL_MAJOR_VERSION, SDL_MINOR_VERSION, SDL_MICRO_VERSION);

        [Macro]
        public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z) => SDL_VERSION >= SDL_VERSIONNUM(X, Y, Z);
    }
}
