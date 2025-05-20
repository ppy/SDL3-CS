// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using static SDL.SDL3;

namespace SDL.Tests
{
    [Explicit("Uses an interactive tray icon.")]
    public unsafe class TestTray : TrayTest
    {
        private volatile bool running;

        [Test]
        public void TestBasic()
        {
            var checkbox = SDL_InsertTrayEntryAt(RootMenu, -1, "Check box?", SDL_TrayEntryFlags.SDL_TRAYENTRY_CHECKBOX);
            Assert.That(checkbox != null, SDL_GetError);
            SetCallback(checkbox, () => Console.WriteLine("Checkbox was toggled."));

            var separator = SDL_InsertTrayEntryAt(RootMenu, -1, (byte*)null, 0);
            Assert.That(separator != null, SDL_GetError);

            var exit = SDL_InsertTrayEntryAt(RootMenu, -1, "Exit tray", SDL_TrayEntryFlags.SDL_TRAYENTRY_BUTTON);
            Assert.That(exit != null, SDL_GetError);
            SetCallback(exit, () => running = false);

            var entries = SDL_GetTrayEntries(RootMenu);
            Assert.That(entries, Is.Not.Null, SDL_GetError);
            Assert.That(entries!.Count, Is.EqualTo(3));

            for (int i = 0; i < entries.Count; i++)
                Console.WriteLine($"{i}. {SDL_GetTrayEntryLabel(entries[i]) ?? "<null>"}");

            running = true;

            while (running)
            {
                SDL_PumpEvents();
            }
        }
    }
}
