// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_AudioDeviceID : UInt32;

    [Typedef]
    public enum SDL_AudioFormat : UInt16
    {
        SDL_AUDIO_U8 = SDL3.SDL_AUDIO_U8,
        SDL_AUDIO_S8 = SDL3.SDL_AUDIO_S8,
        SDL_AUDIO_S16LE = SDL3.SDL_AUDIO_S16LE,
        SDL_AUDIO_S16BE = SDL3.SDL_AUDIO_S16BE,
        SDL_AUDIO_S32LE = SDL3.SDL_AUDIO_S32LE,
        SDL_AUDIO_S32BE = SDL3.SDL_AUDIO_S32BE,
        SDL_AUDIO_F32LE = SDL3.SDL_AUDIO_F32LE,
        SDL_AUDIO_F32BE = SDL3.SDL_AUDIO_F32BE,
    }
}
