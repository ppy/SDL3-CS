// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_HapticID : UInt32;

    [Typedef]
    public enum SDL_HapticEffectType : UInt16
    {
        SDL_HAPTIC_CONSTANT = (UInt16)SDL3.SDL_HAPTIC_CONSTANT,
        SDL_HAPTIC_SINE = (UInt16)SDL3.SDL_HAPTIC_SINE,
        SDL_HAPTIC_SQUARE = (UInt16)SDL3.SDL_HAPTIC_SQUARE,
        SDL_HAPTIC_TRIANGLE = (UInt16)SDL3.SDL_HAPTIC_TRIANGLE,
        SDL_HAPTIC_SAWTOOTHUP = (UInt16)SDL3.SDL_HAPTIC_SAWTOOTHUP,
        SDL_HAPTIC_SAWTOOTHDOWN = (UInt16)SDL3.SDL_HAPTIC_SAWTOOTHDOWN,
        SDL_HAPTIC_RAMP = (UInt16)SDL3.SDL_HAPTIC_RAMP,
        SDL_HAPTIC_SPRING = (UInt16)SDL3.SDL_HAPTIC_SPRING,
        SDL_HAPTIC_DAMPER = (UInt16)SDL3.SDL_HAPTIC_DAMPER,
        SDL_HAPTIC_INERTIA = (UInt16)SDL3.SDL_HAPTIC_INERTIA,
        SDL_HAPTIC_FRICTION = (UInt16)SDL3.SDL_HAPTIC_FRICTION,
        SDL_HAPTIC_LEFTRIGHT = (UInt16)SDL3.SDL_HAPTIC_LEFTRIGHT,
        SDL_HAPTIC_RESERVED1 = (UInt16)SDL3.SDL_HAPTIC_RESERVED1,
        SDL_HAPTIC_RESERVED2 = (UInt16)SDL3.SDL_HAPTIC_RESERVED2,
        SDL_HAPTIC_RESERVED3 = (UInt16)SDL3.SDL_HAPTIC_RESERVED3,
        SDL_HAPTIC_CUSTOM = (UInt16)SDL3.SDL_HAPTIC_CUSTOM,
    }

    [Typedef]
    public enum SDL_HapticDirectionType : byte
    {
        SDL_HAPTIC_POLAR = SDL3.SDL_HAPTIC_POLAR,
        SDL_HAPTIC_CARTESIAN = SDL3.SDL_HAPTIC_CARTESIAN,
        SDL_HAPTIC_SPHERICAL = SDL3.SDL_HAPTIC_SPHERICAL,
        SDL_HAPTIC_STEERING_AXIS = SDL3.SDL_HAPTIC_STEERING_AXIS,
    }

    [Typedef]
    public enum SDL_HapticEffectID : int;

    public static partial class SDL3
    {
        [MustDisposeResource]
        public static unsafe SDLArray<SDL_HapticID>? SDL_GetHaptics()
        {
            int count;
            var array = SDL_GetHaptics(&count);
            return SDLArray.Create(array, count);
        }
    }
}
