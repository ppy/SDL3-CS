// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Drawing;
using NUnit.Framework;
using static SDL.SDL3;

namespace SDL.Tests
{
    [Explicit("Uses an interactive window.")]
    public unsafe class TestPositionalInputVisualisation : MainCallbacksTest
    {
        private SDL_Window* window;
        private SDL_Renderer* renderer;

        protected override SDL_AppResult Init()
        {
            // decouple pen, mouse and touch events
            SDL_SetHint(SDL_HINT_MOUSE_TOUCH_EVENTS, "0");
            SDL_SetHint(SDL_HINT_TOUCH_MOUSE_EVENTS, "0");

            SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO);

            window = SDL_CreateWindow(nameof(TestPositionalInputVisualisation), 1800, 950, SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY);
            renderer = SDL_CreateRenderer(window, (Utf8String)null);

            return base.Init();
        }

        private readonly SortedDictionary<(SDL_TouchID TouchID, SDL_FingerID FingerID), PointF> activeTouches = new SortedDictionary<(SDL_TouchID TouchID, SDL_FingerID FingerID), PointF>();
        private readonly SortedDictionary<SDL_MouseID, PointF> activeMice = new SortedDictionary<SDL_MouseID, PointF>();
        private readonly SortedDictionary<SDL_PenID, PointF> activePens = new SortedDictionary<SDL_PenID, PointF>();

        /// <summary>
        /// Sets a random, but stable color for this object.
        /// </summary>
        private void setColor(object o, byte alpha)
        {
            int color = o.ToString()?.GetHashCode() ?? 0;
            byte b1 = (byte)color;
            byte b2 = (byte)(color / 256);
            byte b3 = (byte)(color / 256 / 256);
            SDL_SetRenderDrawColor(renderer, b1, b2, b3, alpha);
        }

        private void fillRect(RectangleF rect)
        {
            var r = new SDL_FRect { x = rect.X, y = rect.Y, h = rect.Height, w = rect.Width };
            SDL_RenderFillRect(renderer, &r);
        }

        protected override SDL_AppResult Iterate()
        {
            const float gray = 0.1f;
            SDL_SetRenderDrawColorFloat(renderer, gray, gray, gray, 1.0f);
            SDL_RenderClear(renderer);

            // mice are horizontal lines: -
            foreach (var p in activeMice)
            {
                setColor(p.Key, 200);
                RectangleF rect = new RectangleF(p.Value, SizeF.Empty);
                rect.Inflate(50, 20);
                fillRect(rect);
            }

            // fingers are vertical lines: |
            foreach (var p in activeTouches)
            {
                setColor(p.Key, 200);
                RectangleF rect = new RectangleF(p.Value, SizeF.Empty);
                rect.Inflate(20, 50);
                fillRect(rect);
            }

            // pens are squares: □
            foreach (var p in activePens)
            {
                setColor(p.Key, 200);
                RectangleF rect = new RectangleF(p.Value, SizeF.Empty);
                rect.Inflate(30, 30);
                fillRect(rect);
            }

            SDL_RenderPresent(renderer);

            return base.Iterate();
        }

        protected override SDL_AppResult Event(SDL_Event e)
        {
            SDL_ConvertEventToRenderCoordinates(renderer, &e);

            switch (e.Type)
            {
                case SDL_EventType.SDL_EVENT_MOUSE_MOTION:
                    activeMice[e.motion.which] = new PointF(e.motion.x, e.motion.y);
                    break;

                case SDL_EventType.SDL_EVENT_MOUSE_REMOVED:
                    activeMice.Remove(e.mdevice.which);
                    break;

                case SDL_EventType.SDL_EVENT_FINGER_DOWN:
                case SDL_EventType.SDL_EVENT_FINGER_MOTION:
                    activeTouches[(e.tfinger.touchID, e.tfinger.fingerID)] = new PointF(e.tfinger.x, e.tfinger.y);
                    break;

                case SDL_EventType.SDL_EVENT_FINGER_UP:
                    activeTouches.Remove((e.tfinger.touchID, e.tfinger.fingerID));
                    break;

                case SDL_EventType.SDL_EVENT_PEN_MOTION:
                    activePens[e.pmotion.which] = new PointF(e.pmotion.x, e.pmotion.y);
                    break;

                case SDL_EventType.SDL_EVENT_KEY_DOWN:
                    switch (e.key.key)
                    {
                        case SDL_Keycode.SDLK_R:
                            SDL_SetWindowRelativeMouseMode(window, !SDL_GetWindowRelativeMouseMode(window));
                            break;

                        case SDL_Keycode.SDLK_F:
                            SDL_SetWindowFullscreen(window, true);
                            break;

                        case SDL_Keycode.SDLK_W:
                            SDL_SetWindowFullscreen(window, false);
                            break;
                    }

                    break;
            }

            return base.Event(e);
        }
    }
}
