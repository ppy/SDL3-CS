// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL
{
    using unsafe SDL_ClipboardDataCallback = delegate* unmanaged[Cdecl]<IntPtr, byte*, UIntPtr*, IntPtr>;
    using unsafe SDL_ClipboardCleanupCallback = delegate* unmanaged[Cdecl]<IntPtr, void>;

    public partial class SDL3
    {
        [LibraryImport("SDL3", StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe partial int SDL_SetClipboardData(SDL_ClipboardDataCallback callback, SDL_ClipboardCleanupCallback cleanup, IntPtr userdata, string[] mime_types, UIntPtr num_mime_types);

        public static unsafe int SDL_SetClipboardData(SDL_ClipboardDataCallback callback, SDL_ClipboardCleanupCallback cleanup, IntPtr userdata, params string[] mime_types)
            => SDL_SetClipboardData(callback, cleanup, userdata, mime_types, (UIntPtr)mime_types.Length);
    }
}
