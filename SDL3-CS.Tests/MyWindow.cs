// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL;
using static SDL.SDL3;

namespace SDL3.Tests
{
    public sealed unsafe class MyWindow : IDisposable
    {
        private bool flash;
        private ObjectHandle<MyWindow> objectHandle { get; }
        private SDL_Window* sdlWindowHandle;
        private SDL_Renderer* renderer;
        private readonly bool initSuccess;

        private const SDL_InitFlags init_flags = SDL_InitFlags.SDL_INIT_VIDEO | SDL_InitFlags.SDL_INIT_GAMEPAD;

        public MyWindow()
        {
            if (SDL_InitSubSystem(init_flags) < 0)
                throw new InvalidOperationException($"failed to initialise SDL. Error: {SDL_GetError()}");

            initSuccess = true;

            objectHandle = new ObjectHandle<MyWindow>(this, GCHandleType.Normal);
        }

        public void Setup()
        {
            SDL_SetGamepadEventsEnabled(SDL_bool.SDL_TRUE);
            SDL_SetEventFilter(&nativeFilter, objectHandle.Handle);

            if (OperatingSystem.IsWindows())
                SDL_SetWindowsMessageHook(&wndProc, objectHandle.Handle);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static SDL_bool wndProc(IntPtr userdata, MSG* message)
        {
            var handle = new ObjectHandle<MyWindow>(userdata);

            if (handle.GetTarget(out var window))
            {
                Console.WriteLine($"from {window}, message: {message->message}");
            }

            return SDL_TRUE; // sample use of definition from SDL3 class, not SDL_bool enum
        }

        // ReSharper disable once UseCollectionExpression
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static int nativeFilter(IntPtr userdata, SDL_Event* e)
        {
            var handle = new ObjectHandle<MyWindow>(userdata);
            if (handle.GetTarget(out var window))
                return window.handleEventFromFilter(e);

            return 1;
        }

        public Action<SDL_Event>? EventFilter;

        private int handleEventFromFilter(SDL_Event* e)
        {
            switch (e->Type)
            {
                case SDL_EventType.SDL_EVENT_KEY_UP:
                case SDL_EventType.SDL_EVENT_KEY_DOWN:
                    handleKeyFromFilter(e->key);
                    break;

                default:
                    EventFilter?.Invoke(*e);
                    break;
            }

            return 1;
        }

        private void handleKeyFromFilter(SDL_KeyboardEvent e)
        {
            if (e.keysym.sym == SDL_Keycode.SDLK_f)
            {
                flash = true;
            }
        }

        public void Create()
        {
            sdlWindowHandle = SDL_CreateWindow("hello"u8, 800, 600, SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY);
            renderer = SDL_CreateRenderer(sdlWindowHandle, null, SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
        }

        private void handleEvent(SDL_Event e)
        {
            switch (e.Type)
            {
                case SDL_EventType.SDL_EVENT_QUIT:
                    run = false;
                    break;

                case SDL_EventType.SDL_EVENT_KEY_DOWN:
                    switch (e.key.keysym.sym)
                    {
                        case SDL_Keycode.SDLK_r:
                            bool old = SDL_GetRelativeMouseMode() == SDL_bool.SDL_TRUE;
                            SDL_SetRelativeMouseMode(old ? SDL_bool.SDL_FALSE : SDL_bool.SDL_TRUE);
                            break;

                        case SDL_Keycode.SDLK_v:
                            string? text = SDL_GetClipboardText();
                            Console.WriteLine($"clipboard: {text}");
                            break;

                        case SDL_Keycode.SDLK_F10:
                            SDL_SetWindowFullscreen(sdlWindowHandle, SDL_bool.SDL_FALSE);
                            break;

                        case SDL_Keycode.SDLK_F11:
                            SDL_SetWindowFullscreen(sdlWindowHandle, SDL_bool.SDL_TRUE);
                            break;

                        case SDL_Keycode.SDLK_j:
                        {
                            using var gamepads = SDL_GetGamepads();

                            if (gamepads == null || gamepads.Count == 0)
                                break;

                            var gamepad = SDL_OpenGamepad(gamepads[0]);

                            int count;
                            var bindings = SDL_GetGamepadBindings(gamepad, &count);

                            for (int i = 0; i < count; i++)
                            {
                                var binding = *bindings[i];
                                Console.WriteLine(binding.input_type);
                                Console.WriteLine(binding.output_type);
                                Console.WriteLine();
                            }

                            SDL_CloseGamepad(gamepad);
                            break;
                        }

                        case SDL_Keycode.SDLK_F1:
                            SDL_StartTextInput();
                            break;

                        case SDL_Keycode.SDLK_F2:
                            SDL_StopTextInput();
                            break;

                        case SDL_Keycode.SDLK_m:
                            SDL_Keymod mod = e.key.keysym.Mod;
                            Console.WriteLine(mod);
                            break;
                    }

                    break;

                case SDL_EventType.SDL_EVENT_TEXT_INPUT:
                    Console.WriteLine(e.text.GetText());
                    break;

                case SDL_EventType.SDL_EVENT_GAMEPAD_ADDED:
                    Console.WriteLine($"gamepad added: {e.gdevice.which}");
                    break;

                case SDL_EventType.SDL_EVENT_WINDOW_PEN_ENTER:
                    SDL_PenCapabilityInfo info;
                    var cap = (SDL_PEN_CAPABILITIES)SDL_GetPenCapabilities((SDL_PenID)e.window.data1, &info);

                    if (cap.HasFlag(SDL_PEN_CAPABILITIES.SDL_PEN_AXIS_XTILT_MASK))
                        Console.WriteLine("has pen xtilt axis");

                    Console.WriteLine(info.max_tilt);
                    break;
            }
        }

        private bool run = true;

        private const int events_per_peep = 64;
        private readonly SDL_Event[] events = new SDL_Event[events_per_peep];

        private void pollEvents()
        {
            SDL_PumpEvents();

            int eventsRead;

            do
            {
                eventsRead = SDL_PeepEvents(events, SDL_eventaction.SDL_GETEVENT, SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);
                for (int i = 0; i < eventsRead; i++)
                    handleEvent(events[i]);
            } while (eventsRead == events_per_peep);
        }

        private float frame;

        public void Run()
        {
            while (run)
            {
                if (flash)
                {
                    flash = false;
                    Console.WriteLine("flash!");
                }

                pollEvents();

                SDL_SetRenderDrawColorFloat(renderer, SDL_sinf(frame) / 2 + 0.5f, SDL_cosf(frame) / 2 + 0.5f, 0.3f, 1.0f);
                SDL_RenderClear(renderer);
                SDL_RenderPresent(renderer);

                frame += 0.015f;

                Thread.Sleep(10);
            }
        }

        public void Dispose()
        {
            if (initSuccess)
                SDL_QuitSubSystem(init_flags);

            objectHandle.Dispose();
        }
    }
}
