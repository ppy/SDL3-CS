// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.InteropServices;
using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestSDLArray
    {
        private static unsafe T* CopyToSdl<T>(T[] array)
            where T : unmanaged
        {
            UIntPtr size = (UIntPtr)(Marshal.SizeOf<T>() * array.Length);
            IntPtr target = SDL3.SDL_malloc(size);

            fixed (T* source = array)
            {
                SDL3.SDL_memcpy(target, (IntPtr)source, size);
            }

            return (T*)target;
        }

        private static unsafe T** CopyToSdl<T>(T*[] array)
            where T : unmanaged
        {
            UIntPtr size = (UIntPtr)(sizeof(IntPtr) * array.Length);
            IntPtr target = SDL3.SDL_malloc(size);

            fixed (T** source = array)
            {
                SDL3.SDL_memcpy(target, (IntPtr)source, size);
            }

            return (T**)target;
        }

        [Test]
        public unsafe void TestArrayEnumerator()
        {
            int[] values = [10, 20, 30, 40];
            int* sdlMemory = CopyToSdl(values);

            using var array = new SDLArray<int>(sdlMemory, values.Length);
            int index = 0;

            foreach (int i in array)
            {
                Assert.AreEqual(values[index++], i);
            }
        }

        [Test]
        public unsafe void TestConstOpaquePointerArrayEnumerator()
        {
            int a = 10, b = 20, c = 30, d = 40;
            int*[] values = [&a, &b, &c, &d];
            int** sdlMemory = null;

            // Const pointer arrays are not freed automatically. Since the
            // unit test owns the memory, this must be done at the end of the
            // test.

            try
            {
                sdlMemory = CopyToSdl(values);

                var array = new SDLConstOpaquePointerArray<int>(sdlMemory, values.Length);
                int index = 0;

                foreach (int* i in array)
                {
                    Assert.AreEqual((IntPtr)values[index++], (IntPtr)i);
                }
            }
            finally
            {
                if (sdlMemory != null)
                    SDL3.SDL_free(sdlMemory);
            }
        }

        [Test]
        public unsafe void TestOpaquePointerArrayEnumerator()
        {
            int a = 10, b = 20, c = 30, d = 40;
            int*[] values = [&a, &b, &c, &d];
            int** sdlMemory = CopyToSdl(values);

            using var array = new SDLOpaquePointerArray<int>(sdlMemory, values.Length);
            int index = 0;

            foreach (int* i in array)
            {
                Assert.AreEqual((IntPtr)values[index++], (IntPtr)i);
            }
        }

        [Test]
        public unsafe void TestPointerArrayEnumerator()
        {
            int a = 10, b = 20, c = 30, d = 40;
            int*[] values = [&a, &b, &c, &d];
            int** memory = CopyToSdl(values);

            using var array = new SDLPointerArray<int>(memory, values.Length);
            int index = 0;

            foreach (int i in array)
            {
                Assert.AreEqual(*values[index++], i);
            }
        }
    }
}
