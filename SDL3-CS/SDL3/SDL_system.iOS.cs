// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.Versioning;

namespace SDL
{
    public static partial class SDL3
    {
        [SupportedOSPlatform("iOS")]
        [Macro]
        public static unsafe int SDL_iOSSetAnimationCallback(SDL_Window* window, int interval, delegate* unmanaged[Cdecl]<IntPtr, void> callback, IntPtr callbackParam)
            => SDL_iPhoneSetAnimationCallback(window, interval, callback, callbackParam);

        [SupportedOSPlatform("iOS")]
        [Macro]
        public static void SDL_iOSSetEventPump(SDL_bool enabled)
            => SDL_iPhoneSetEventPump(enabled);
    }
}
