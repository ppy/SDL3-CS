// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum TTF_FontStyleFlags : UInt32
    {
        TTF_STYLE_NORMAL = SDL3_ttf.TTF_STYLE_NORMAL,
        TTF_STYLE_BOLD = SDL3_ttf.TTF_STYLE_BOLD,
        TTF_STYLE_ITALIC = SDL3_ttf.TTF_STYLE_ITALIC,
        TTF_STYLE_UNDERLINE = SDL3_ttf.TTF_STYLE_UNDERLINE,
        TTF_STYLE_STRIKETHROUGH = SDL3_ttf.TTF_STYLE_STRIKETHROUGH,
    }

    [Flags]
    [Typedef]
    public enum TTF_SubStringFlags : UInt32
    {
        TTF_SUBSTRING_DIRECTION_MASK = SDL3_ttf.TTF_SUBSTRING_DIRECTION_MASK,
        TTF_SUBSTRING_TEXT_START = SDL3_ttf.TTF_SUBSTRING_TEXT_START,
        TTF_SUBSTRING_LINE_START = SDL3_ttf.TTF_SUBSTRING_LINE_START,
        TTF_SUBSTRING_LINE_END = SDL3_ttf.TTF_SUBSTRING_LINE_END,
        TTF_SUBSTRING_TEXT_END = SDL3_ttf.TTF_SUBSTRING_TEXT_END,
    }

    public static unsafe partial class SDL3_ttf
    {
        [Constant]
        public static readonly int SDL_TTF_VERSION = SDL3.SDL_VERSIONNUM(SDL_TTF_MAJOR_VERSION, SDL_TTF_MINOR_VERSION, SDL_TTF_MICRO_VERSION);

        [Macro]
        public static bool SDL_TTF_VERSION_ATLEAST(int X, int Y, int Z) =>
            ((SDL_TTF_MAJOR_VERSION >= X) &&
             (SDL_TTF_MAJOR_VERSION > X || SDL_TTF_MINOR_VERSION >= Y) &&
             (SDL_TTF_MAJOR_VERSION > X || SDL_TTF_MINOR_VERSION > Y || SDL_TTF_MICRO_VERSION >= Z));
    }
}
