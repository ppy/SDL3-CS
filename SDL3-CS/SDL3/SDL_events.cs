﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Text;

namespace SDL
{
    public partial struct SDL_CommonEvent
    {
        public SDL_EventType Type => (SDL_EventType)type;
    }

    public partial struct SDL_UserEvent
    {
        public SDL_EventType Type => (SDL_EventType)type;
    }

    public partial struct SDL_Event
    {
        public SDL_EventType Type => (SDL_EventType)type;
    }

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

    public static partial class SDL3
    {
        public static unsafe int SDL_PeepEvents(SDL_Event[] events, SDL_EventAction action, SDL_EventType minType, SDL_EventType maxType)
        {
            fixed (SDL_Event* eventsPtr = events)
                return SDL_PeepEvents(eventsPtr, events.Length, action, (uint)minType, (uint)maxType);
        }

        public static SDLBool SDL_HasEvent(SDL_EventType type) => SDL_HasEvent((uint)type);
        public static void SDL_FlushEvent(SDL_EventType type) => SDL_FlushEvent((uint)type);
        public static void SDL_FlushEvents(SDL_EventType minType, SDL_EventType maxType) => SDL_FlushEvents((uint)minType, (uint)maxType);
        public static void SDL_SetEventEnabled(SDL_EventType type, bool enabled) => SDL_SetEventEnabled((uint)type, enabled);
        public static SDLBool SDL_EventEnabled(SDL_EventType type) => SDL_EventEnabled((uint)type);

        public static string SDL_GetEventDescription(SDL_Event @event)
        {
            // Buffer size taken from https://github.com/libsdl-org/SDL/blob/7dd5e765df239986f78c9b0016e3f3023d885084/src/events/SDL_events.c#L908-L913.
            const int bufferSize = 256;
            Span<byte> buf = stackalloc byte[bufferSize];

            int bytesWritten;

            unsafe
            {
                fixed (byte* ptr = buf)
                    bytesWritten = SDL_GetEventDescription(&@event, ptr, bufferSize);
            }

            int bytesToRead = bytesWritten > bufferSize ? bufferSize : bytesWritten;
            return Encoding.UTF8.GetString(buf[..bytesToRead]);
        }
    }
}
