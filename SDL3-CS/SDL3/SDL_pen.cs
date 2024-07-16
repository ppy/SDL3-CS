// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_PenID : UInt32;

    [Flags]
    [Typedef]
    public enum SDL_PenCapabilityFlags : uint
    {
        SDL_PEN_DOWN_MASK = SDL3.SDL_PEN_DOWN_MASK,
        SDL_PEN_INK_MASK = SDL3.SDL_PEN_INK_MASK,
        SDL_PEN_ERASER_MASK = SDL3.SDL_PEN_ERASER_MASK,
        SDL_PEN_AXIS_PRESSURE_MASK = SDL3.SDL_PEN_AXIS_PRESSURE_MASK,
        SDL_PEN_AXIS_XTILT_MASK = SDL3.SDL_PEN_AXIS_XTILT_MASK,
        SDL_PEN_AXIS_YTILT_MASK = SDL3.SDL_PEN_AXIS_YTILT_MASK,
        SDL_PEN_AXIS_DISTANCE_MASK = SDL3.SDL_PEN_AXIS_DISTANCE_MASK,
        SDL_PEN_AXIS_ROTATION_MASK = SDL3.SDL_PEN_AXIS_ROTATION_MASK,
        SDL_PEN_AXIS_SLIDER_MASK = SDL3.SDL_PEN_AXIS_SLIDER_MASK,
        SDL_PEN_AXIS_BIDIRECTIONAL_MASKS = SDL3.SDL_PEN_AXIS_BIDIRECTIONAL_MASKS,
    }

    public static partial class SDL3
    {
        [Constant]
        public const SDL_MouseID SDL_PEN_MOUSEID = unchecked((SDL_MouseID)(-2));

        [Macro]
        public static SDL_PenCapabilityFlags SDL_PEN_CAPABILITY(int capbit) => (SDL_PenCapabilityFlags)(1ul << (capbit));

        [Macro]
        public static SDL_PenCapabilityFlags SDL_PEN_AXIS_CAPABILITY(SDL_PenAxis axis) => SDL_PEN_CAPABILITY((int)axis + SDL_PEN_FLAG_AXIS_BIT_OFFSET);

        [MustDisposeResource]
        public static unsafe SDLArray<SDL_PenID>? SDL_GetPens()
        {
            int count;
            var array = SDL_GetPens(&count);
            return SDLArray.Create(array, count);
        }
    }
}
