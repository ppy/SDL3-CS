// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public partial class SDL3
    {
        [Macro]
        public static unsafe bool SDL_QuitRequested()
        {
            SDL_PumpEvents();
            return SDL_PeepEvents(null, 0, SDL_eventaction.SDL_PEEKEVENT, SDL_EventType.SDL_EVENT_QUIT, SDL_EventType.SDL_EVENT_QUIT) > 0;
        }
    }
}
