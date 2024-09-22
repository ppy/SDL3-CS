// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace SDL.Tests
{
    /// <summary>
    /// Base class for tests that use SDL3 main callbacks.
    /// See https://wiki.libsdl.org/SDL3/README/main-functions#how-to-use-main-callbacks-in-sdl3.
    /// </summary>
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public abstract unsafe class MainCallbacksTest
    {
        [Test]
        public void TestEnterMainCallbacks()
        {
            var objectHandle = new ObjectHandle<MainCallbacksTest>(this, GCHandleType.Normal);
            SDL3.SDL_EnterAppMainCallbacks(0, (byte**)objectHandle.Handle, &AppInit, &AppIterate, &AppEvent, &AppQuit);
        }

        protected virtual SDL_AppResult Init()
        {
            SDL3.SDL_SetLogPriorities(SDL_LogPriority.SDL_LOG_PRIORITY_VERBOSE);
            SDL3.SDL_SetLogOutputFunction(&LogOutput, IntPtr.Zero);
            return SDL_AppResult.SDL_APP_CONTINUE;
        }

        protected virtual SDL_AppResult Iterate()
        {
            Thread.Sleep(10);
            return SDL_AppResult.SDL_APP_CONTINUE;
        }

        protected virtual SDL_AppResult Event(SDL_Event e)
        {
            switch (e.Type)
            {
                case SDL_EventType.SDL_EVENT_QUIT:
                case SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED:
                case SDL_EventType.SDL_EVENT_TERMINATING:
                case SDL_EventType.SDL_EVENT_KEY_DOWN when e.key.key == SDL_Keycode.SDLK_ESCAPE:
                    return SDL_AppResult.SDL_APP_SUCCESS;
            }

            return SDL_AppResult.SDL_APP_CONTINUE;
        }

        protected virtual void Quit()
        {
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void LogOutput(IntPtr userdata, int category, SDL_LogPriority priority, byte* message)
        {
            Console.WriteLine(SDL3.PtrToStringUTF8(message));
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static SDL_AppResult AppInit(IntPtr* appState, int argc, byte** argv)
        {
            IntPtr handle = (IntPtr)argv;
            *appState = handle;

            var objectHandle = new ObjectHandle<MainCallbacksTest>(handle, true);
            if (objectHandle.GetTarget(out var target))
                return target.Init();

            return SDL_AppResult.SDL_APP_FAILURE;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static SDL_AppResult AppIterate(IntPtr appState)
        {
            var objectHandle = new ObjectHandle<MainCallbacksTest>(appState, true);
            if (objectHandle.GetTarget(out var target))
                return target.Iterate();

            return SDL_AppResult.SDL_APP_FAILURE;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static SDL_AppResult AppEvent(IntPtr appState, SDL_Event* e)
        {
            var objectHandle = new ObjectHandle<MainCallbacksTest>(appState, true);
            if (objectHandle.GetTarget(out var target))
                return target.Event(*e);

            return SDL_AppResult.SDL_APP_FAILURE;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void AppQuit(IntPtr appState)
        {
            using var objectHandle = new ObjectHandle<MainCallbacksTest>(appState, true);
            if (objectHandle.GetTarget(out var target))
                target.Quit();
        }
    }
}
