﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;

namespace SDL
{
    public sealed unsafe class SDLConstOpaquePointerArray<T>
        where T : unmanaged
    {
        private readonly T** array;
        public readonly int Count;

        internal SDLConstOpaquePointerArray(T** array, int count)
        {
            this.array = array;
            Count = count;
        }

        public T* this[int index]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
                Debug.Assert(array[index] != null);
                return array[index];
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        public ref struct Enumerator
        {
            private readonly SDLConstOpaquePointerArray<T> array;
            private int index;

            internal Enumerator(SDLConstOpaquePointerArray<T> array)
            {
                this.array = array;
                index = -1;
            }

            public bool MoveNext() => ++index < array.Count;

            public T* Current => array[index];
        }
    }
}
