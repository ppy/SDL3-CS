// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public partial struct SDL_MessageBoxButtonData
    {
        public SDL_MessageBoxButtonFlags Flags => (SDL_MessageBoxButtonFlags)flags;
    }

    public partial struct SDL_MessageBoxData
    {
        public SDL_MessageBoxFlags Flags => (SDL_MessageBoxFlags)flags;
    }

    public static partial class SDL3
    {
        // public static int SDL_ShowSimpleMessageBox([NativeTypeName("Uint32")] uint flags, [NativeTypeName("const char *")] byte* title, [NativeTypeName("const char *")] byte* message, SDL_Window* window);
        public static unsafe int SDL_ShowSimpleMessageBox(SDL_MessageBoxFlags flags, Utf8String title, Utf8String message, SDL_Window* window)
            => SDL_ShowSimpleMessageBox((uint)flags, title, message, window);
    }
}
