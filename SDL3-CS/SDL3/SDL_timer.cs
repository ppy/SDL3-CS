// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Typedef]
    public enum SDL_TimerID : UInt32;

    public static partial class SDL3
    {
        [Macro]
        public static UInt64 SDL_SECONDS_TO_NS(UInt64 S) => (((UInt64)(S)) * SDL_NS_PER_SECOND);

        [Macro]
        public static UInt64 SDL_NS_TO_SECONDS(UInt64 NS) => ((NS) / SDL_NS_PER_SECOND);

        [Macro]
        public static UInt64 SDL_MS_TO_NS(UInt64 MS) => (((UInt64)(MS)) * SDL_NS_PER_MS);

        [Macro]
        public static UInt64 SDL_NS_TO_MS(UInt64 NS) => ((NS) / SDL_NS_PER_MS);

        [Macro]
        public static UInt64 SDL_US_TO_NS(UInt64 US) => (((UInt64)(US)) * SDL_NS_PER_US);

        [Macro]
        public static UInt64 SDL_NS_TO_US(UInt64 NS) => ((NS) / SDL_NS_PER_US);
    }
}
