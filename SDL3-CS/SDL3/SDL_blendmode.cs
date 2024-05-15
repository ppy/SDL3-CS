// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_BlendMode : UInt32
    {
        SDL_BLENDMODE_NONE = SDL3.SDL_BLENDMODE_NONE,
        SDL_BLENDMODE_BLEND = SDL3.SDL_BLENDMODE_BLEND,
        SDL_BLENDMODE_ADD = SDL3.SDL_BLENDMODE_ADD,
        SDL_BLENDMODE_MOD = SDL3.SDL_BLENDMODE_MOD,
        SDL_BLENDMODE_MUL = SDL3.SDL_BLENDMODE_MUL,
        SDL_BLENDMODE_INVALID = SDL3.SDL_BLENDMODE_INVALID,
    }
}
