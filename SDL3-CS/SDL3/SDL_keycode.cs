// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static SDL_Keycode SDL_SCANCODE_TO_KEYCODE(SDL_Scancode scancode) => (SDL_Keycode)((int)scancode | SDLK_SCANCODE_MASK);
    }
}
