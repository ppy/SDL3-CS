// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum TTF_SubStringFlags : int
    {
        TTF_SUBSTRING_TEXT_START = SDL3_ttf.TTF_SUBSTRING_TEXT_START,
        TTF_SUBSTRING_LINE_START = SDL3_ttf.TTF_SUBSTRING_LINE_START,
        TTF_SUBSTRING_LINE_END = SDL3_ttf.TTF_SUBSTRING_LINE_END,
        TTF_SUBSTRING_TEXT_END = SDL3_ttf.TTF_SUBSTRING_TEXT_END,
    }

    public static unsafe partial class SDL3_ttf
    {
        [Constant]
        public static readonly int SDL_TTF_VERSION = SDL3.SDL_VERSIONNUM(SDL_TTF_MAJOR_VERSION, SDL_TTF_MINOR_VERSION, SDL_TTF_MICRO_VERSION);

#pragma warning disable CA2255
        [ModuleInitializer]
        internal static void ModuleInitializer()
        {
            SDL3.SDL_Init(0);
        }
#pragma warning restore CA2255
    }
}
