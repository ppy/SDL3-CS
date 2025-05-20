// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_PenID : UInt32;

    [Flags]
    [Typedef]
    public enum SDL_PenInputFlags : UInt32
    {
        SDL_PEN_INPUT_DOWN = SDL3.SDL_PEN_INPUT_DOWN,
        SDL_PEN_INPUT_BUTTON_1 = SDL3.SDL_PEN_INPUT_BUTTON_1,
        SDL_PEN_INPUT_BUTTON_2 = SDL3.SDL_PEN_INPUT_BUTTON_2,
        SDL_PEN_INPUT_BUTTON_3 = SDL3.SDL_PEN_INPUT_BUTTON_3,
        SDL_PEN_INPUT_BUTTON_4 = SDL3.SDL_PEN_INPUT_BUTTON_4,
        SDL_PEN_INPUT_BUTTON_5 = SDL3.SDL_PEN_INPUT_BUTTON_5,
        SDL_PEN_INPUT_ERASER_TIP = SDL3.SDL_PEN_INPUT_ERASER_TIP,
    }

    public static partial class SDL3
    {
        [Constant]
        public const SDL_MouseID SDL_PEN_MOUSEID = unchecked((SDL_MouseID)(-2));

        [Constant]
        public const SDL_TouchID SDL_PEN_TOUCHID = unchecked((SDL_TouchID)(-2));
    }
}
