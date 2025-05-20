// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public abstract unsafe class TrayTest
    {
        private SDL_Tray* tray;
        protected SDL_TrayMenu* RootMenu { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Assert.That(SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO), SDL3.SDL_GetError);
            tray = SDL3.SDL_CreateTray(null, "Test tray");
            Assert.That(tray != null, SDL3.SDL_GetError);
            RootMenu = SDL3.SDL_CreateTrayMenu(tray);
            Assert.That(RootMenu != null, SDL3.SDL_GetError);
        }

        protected static void SetCallback(SDL_TrayEntry* entry, Action callback)
        {
            var objectHandle = new ObjectHandle<Action>(callback, GCHandleType.Normal);
            SDL3.SDL_SetTrayEntryCallback(entry, &nativeOnSelect, objectHandle.Handle); // this is leaking object handles, fine for tests
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void nativeOnSelect(IntPtr userdata, SDL_TrayEntry* entry)
        {
            var objectHandle = new ObjectHandle<Action>(userdata, true);

            if (objectHandle.GetTarget(out var action))
                action();
            else
                Assert.Fail("Accessing disposed object handle.");
        }

        [TearDown]
        public void TearDown()
        {
            if (tray != null)
                SDL3.SDL_DestroyTray(tray);

            SDL3.SDL_Quit();
        }
    }
}
