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
    public enum SDL_PropertyType
    {
        SDL_PROPERTY_TYPE_INVALID,
        SDL_PROPERTY_TYPE_POINTER,
        SDL_PROPERTY_TYPE_STRING,
        SDL_PROPERTY_TYPE_NUMBER,
        SDL_PROPERTY_TYPE_FLOAT,
        SDL_PROPERTY_TYPE_BOOLEAN,
    }

    public static unsafe partial class SDL3
    {
        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_PropertiesID SDL_GetGlobalProperties();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_PropertiesID SDL_CreateProperties();

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_CopyProperties(SDL_PropertiesID src, SDL_PropertiesID dst);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_LockProperties(SDL_PropertiesID props);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SDL_UnlockProperties(SDL_PropertiesID props);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetPointerPropertyWithCleanup(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("void*")] IntPtr value, [NativeTypeName("SDL_CleanupPropertyCallback")] delegate* unmanaged[Cdecl]<IntPtr, IntPtr, void> cleanup, [NativeTypeName("void*")] IntPtr userdata);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetPointerProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("void*")] IntPtr value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetStringProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("const char *")] byte* value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetNumberProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("Sint64")] long value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetFloatProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, float value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_SetBooleanProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("bool")] SDLBool value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_HasProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_PropertyType SDL_GetPropertyType(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("void*")]
        public static extern IntPtr SDL_GetPointerProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("void*")] IntPtr default_value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetStringProperty", ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern byte* Unsafe_SDL_GetStringProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("const char *")] byte* default_value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("Sint64")]
        public static extern long SDL_GetNumberProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("Sint64")] long default_value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float SDL_GetFloatProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, float default_value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_GetBooleanProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name, [NativeTypeName("bool")] SDLBool default_value);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_ClearProperty(SDL_PropertiesID props, [NativeTypeName("const char *")] byte* name);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("bool")]
        public static extern SDLBool SDL_EnumerateProperties(SDL_PropertiesID props, [NativeTypeName("SDL_EnumeratePropertiesCallback")] delegate* unmanaged[Cdecl]<IntPtr, SDL_PropertiesID, byte*, void> callback, [NativeTypeName("void*")] IntPtr userdata);

        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void SDL_DestroyProperties(SDL_PropertiesID props);
    }
}
