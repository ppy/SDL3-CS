// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_InitFlags : UInt32
    {
        SDL_INIT_TIMER = SDL3.SDL_INIT_TIMER,
        SDL_INIT_AUDIO = SDL3.SDL_INIT_AUDIO,
        SDL_INIT_VIDEO = SDL3.SDL_INIT_VIDEO,
        SDL_INIT_JOYSTICK = SDL3.SDL_INIT_JOYSTICK,
        SDL_INIT_HAPTIC = SDL3.SDL_INIT_HAPTIC,
        SDL_INIT_GAMEPAD = SDL3.SDL_INIT_GAMEPAD,
        SDL_INIT_EVENTS = SDL3.SDL_INIT_EVENTS,
        SDL_INIT_SENSOR = SDL3.SDL_INIT_SENSOR,
        SDL_INIT_CAMERA = SDL3.SDL_INIT_CAMERA,
    }
}
