// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    public partial struct SDL_RendererInfo
    {
        public SDL_RendererFlags Flags => (SDL_RendererFlags)flags;
    }

    public static partial class SDL3
    {
        public static unsafe SDL_Renderer* SDL_CreateRenderer(SDL_Window* window, ReadOnlySpan<byte> name, SDL_RendererFlags flags)
            => SDL_CreateRenderer(window, name, (uint)flags);
    }
}
