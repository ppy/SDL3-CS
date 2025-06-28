// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using static SDL.SDL3;

namespace SDL
{
    public static unsafe partial class SDL3_mixer
    {
        [Constant]
        public static readonly SDL_AudioFormat MIX_DEFAULT_FORMAT = SDL_AUDIO_S16;
    }
}
