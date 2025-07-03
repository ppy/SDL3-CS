// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using System.Text;
using static SDL.SDL3_image;
using static SDL.SDL3_ttf;
using static SDL.SDL3_mixer;
using static SDL.SDL3;

namespace SDL.Tests
{
    public static class Program
    {
        public static void Main()
        {
            if (OperatingSystem.IsWindows())
                Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine($"false is represented as {SDL_OutOfMemory()} (expected 0x{SDLBool.FALSE_VALUE:x2})");
            Console.WriteLine($"true  is represented as {SDL_ClearError()} (expected 0x{SDLBool.TRUE_VALUE:x2})");

            SDL_SetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4, "null byte \0 in string"u8);
            Debug.Assert(SDL_GetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4) == "null byte ");

            SDL_SetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4, "1"u8);
            SDL_SetHint(SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4, "1");

            using (var window = new MyWindow())
            {
                // Check if satellite libraries exist.
                Console.WriteLine($"SDL revision: {SDL_GetRevision()}, IMG {IMG_Version()}, TTF {TTF_Version()}, Mixer {Mix_Version()}");

                printDisplays();

                window.Setup();
                window.Create();

                printWindows();

                const SDL_Keymod state = SDL_Keymod.SDL_KMOD_CAPS | SDL_Keymod.SDL_KMOD_ALT;
                SDL_SetModState(state);
                Debug.Assert(SDL_GetModState() == state);

                window.Run();
            }

            SDL_Quit();
        }

        private static void printDisplays()
        {
            using var displays = SDL_GetDisplays();
            if (displays == null)
                return;

            for (int i = 0; i < displays.Count; i++)
            {
                SDL_DisplayID id = displays[i];
                Console.WriteLine(id);

                using var modes = SDL_GetFullscreenDisplayModes(id);
                if (modes == null)
                    continue;

                for (int j = 0; j < modes.Count; j++)
                {
                    SDL_DisplayMode mode = modes[j];
                    Console.WriteLine($"{mode.w}x{mode.h}@{mode.refresh_rate}");
                }
            }
        }

        private static unsafe void printWindows()
        {
            using var windows = SDL_GetWindows();
            if (windows == null)
                return;

            for (int i = 0; i < windows.Count; i++)
            {
                Console.WriteLine($"Window {i} title: {SDL_GetWindowTitle(windows[i])}");
            }
        }
    }
}
