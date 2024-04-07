// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static void SDL_VERSION(ref SDL_Version version)
        {
            version.major = SDL_MAJOR_VERSION;
            version.minor = SDL_MINOR_VERSION;
            version.patch = SDL_PATCHLEVEL;
        }

        [Macro]
        public static int SDL_VERSIONNUM(int X, int Y, int Z) => ((X) << 24 | (Y) << 8 | (Z) << 0);

        [Macro]
        public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z) => SDL_COMPILEDVERSION >= SDL_VERSIONNUM(X, Y, Z);
    }
}
