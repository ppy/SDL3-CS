/*
  <auto-generated/>
  C# bindings for Simple DirectMedia Layer.
  Original copyright notice of input files:

  Simple DirectMedia Layer
  Copyright (C) 1997-2024 Sam Lantinga <slouken@libsdl.org>

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Runtime.InteropServices;

namespace SDL
{
    public static unsafe partial class SDL3
    {
        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_SetClipboardText([NativeTypeName("const char *")] byte* text);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetClipboardText();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_bool")]
        public static extern int SDL_HasClipboardText();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_SetPrimarySelectionText([NativeTypeName("const char *")] byte* text);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("char *")]
        public static extern byte* SDL_GetPrimarySelectionText();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_bool")]
        public static extern int SDL_HasPrimarySelectionText();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_SetClipboardData([NativeTypeName("SDL_ClipboardDataCallback")] delegate* unmanaged[Cdecl]<IntPtr, byte*, nuint*, IntPtr> callback, [NativeTypeName("SDL_ClipboardCleanupCallback")] delegate* unmanaged[Cdecl]<IntPtr, void> cleanup, [NativeTypeName("void*")] IntPtr userdata, [NativeTypeName("const char **")] byte** mime_types, [NativeTypeName("size_t")] nuint num_mime_types);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int SDL_ClearClipboardData();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("void*")]
        public static extern IntPtr SDL_GetClipboardData([NativeTypeName("const char *")] byte* mime_type, [NativeTypeName("size_t *")] nuint* size);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("SDL_bool")]
        public static extern int SDL_HasClipboardData([NativeTypeName("const char *")] byte* mime_type);
    }
}