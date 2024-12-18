// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace SDL
{
    [MustDisposeResource]
    public sealed unsafe class SDLOpaquePointerArray<T> : IDisposable
        where T : unmanaged
    {
        private readonly T** array;
        public readonly int Count;
        private bool isDisposed;

        internal SDLOpaquePointerArray(T** array, int count)
        {
            this.array = array;
            Count = count;
        }

        public T* this[int index]
        {
            get
            {
                ObjectDisposedException.ThrowIf(isDisposed, this);
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
                Debug.Assert(array[index] != null);
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
}
