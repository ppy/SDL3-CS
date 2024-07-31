// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [MustDisposeResource]
    public sealed unsafe class SDLArray<T> : IDisposable
        where T : unmanaged
    {
        private readonly T* array;
        public readonly int Count;
        private bool isDisposed;

        internal SDLArray(T* array, int count)
        {
            this.array = array;
            Count = count;
        }

        public T this[int index]
        {
            get
            {
                ObjectDisposedException.ThrowIf(isDisposed, this);
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
                return array[index];
            }
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            isDisposed = true;

            SDL3.SDL_free(array);
        }
    }

    internal static unsafe class SDLArray
    {
        [MustDisposeResource]
        internal static SDLArray<T>? Create<T>(T* array, int count)
            where T : unmanaged
        {
            if (array == null)
                return null;

            return new SDLArray<T>(array, count);
        }

        [MustDisposeResource]
        internal static SDLPointerArray<T>? Create<T>(T** array, int count)
            where T : unmanaged
        {
            if (array == null)
                return null;

            return new SDLPointerArray<T>(array, count);
        }
    }
}
