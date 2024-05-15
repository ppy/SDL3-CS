// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    [Flags]
    [Typedef]
    public enum SDL_GlobFlags : UInt32
    {
        SDL_GLOB_CASEINSENSITIVE = SDL3.SDL_GLOB_CASEINSENSITIVE,
    }
}
