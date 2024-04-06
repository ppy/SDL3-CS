// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_FingerID : UInt64;

    [Typedef]
    public enum SDL_TouchID : UInt64;

    public static partial class SDL3
    {
        [Constant]
        public const SDL_MouseID SDL_TOUCH_MOUSEID = unchecked((SDL_MouseID)(-1));

        [Constant]
        public const SDL_TouchID SDL_MOUSE_TOUCHID = unchecked((SDL_TouchID)(-1));
    }
}
