// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public unsafe partial struct SDL_TextInputEvent
    {
        public string? GetText() => SDL3.PtrToStringUTF8(text);
    }

    public unsafe partial struct SDL_TextEditingEvent
    {
        public string? GetText() => SDL3.PtrToStringUTF8(text);
    }

    public unsafe partial struct SDL_DropEvent
    {
        public string? GetSource() => SDL3.PtrToStringUTF8(source);

        public string? GetData() => SDL3.PtrToStringUTF8(data);
    }

    public partial struct SDL_MouseButtonEvent
    {
        public SDLButton Button => (SDLButton)button;
    }

    public partial struct SDL_GamepadAxisEvent
    {
        public SDL_GamepadAxis Axis => (SDL_GamepadAxis)axis;
    }

    public partial struct SDL_GamepadButtonEvent
    {
        public SDL_GamepadButton Button => (SDL_GamepadButton)button;
    }
}
