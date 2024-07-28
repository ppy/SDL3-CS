// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace SDL
{
    public static partial class SDL3
    {
        /// <returns>
        /// An array of <see cref="nint"/> that can be passed to <see cref="Marshal.PtrToStringUTF8(System.IntPtr)"/>.
        /// </returns>
        [MustDisposeResource]
        public static unsafe SDLArray<IntPtr>? SDL_GlobStorageDirectory(SDL_Storage* storage, byte* path, byte* pattern, SDL_GlobFlags flags)
        {
            int count;
            IntPtr* array = (IntPtr*)SDL_GlobStorageDirectory(storage, path, pattern, flags, &count);
            return SDLArray.Create(array, count);
        }
    }
}
