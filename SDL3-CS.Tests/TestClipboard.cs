// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestClipboard
    {
        [SetUp]
        public void SetUp()
        {
            cleanups = 0;
            requestedMimeTypes.Clear();
            SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO);
        }

        [TearDown]
        public void TearDown() => SDL3.SDL_Quit();

        [Test]
        public unsafe void TestClipboardData()
        {
            var ret = SDL3.SDL_SetClipboardData(&dataCallback, &cleanupCallback, IntPtr.Zero, "test/one", "test/two");
            Assert.That(ret, Is.EqualTo(SDL_bool.SDL_TRUE), SDL3.SDL_GetError);

            Assert.That(SDL3.SDL_HasClipboardData("test/one"), Is.EqualTo(SDL_bool.SDL_TRUE));
            Assert.That(SDL3.SDL_HasClipboardData("test/two"), Is.EqualTo(SDL_bool.SDL_TRUE));
            Assert.That(SDL3.SDL_HasClipboardData("test/three"), Is.EqualTo(SDL_bool.SDL_FALSE));

            UIntPtr size;
            IntPtr data = SDL3.SDL_GetClipboardData("test/one", &size);

            try
            {
                Assert.That(data, Is.EqualTo(IntPtr.Zero));
                Assert.That(size, Is.EqualTo(my_length));
                Assert.That(requestedMimeTypes.Dequeue(), Is.EqualTo("test/one"));
            }
            finally
            {
                SDL3.SDL_free(data);
            }

            ret = SDL3.SDL_ClearClipboardData();
            Assert.That(ret, Is.EqualTo(SDL_bool.SDL_TRUE), SDL3.SDL_GetError);

            Assert.That(cleanups, Is.EqualTo(1));

            Assert.That(SDL3.SDL_HasClipboardData("test/one"), Is.EqualTo(SDL_bool.SDL_FALSE));
            Assert.That(SDL3.SDL_HasClipboardData("test/two"), Is.EqualTo(SDL_bool.SDL_FALSE));
            Assert.That(SDL3.SDL_HasClipboardData("test/three"), Is.EqualTo(SDL_bool.SDL_FALSE));

            data = SDL3.SDL_GetClipboardData("test/two", &size);

            try
            {
                Assert.That(data, Is.EqualTo(IntPtr.Zero));
                Assert.That(size, Is.EqualTo((UIntPtr)0));
                Assert.That(requestedMimeTypes, Has.Count.EqualTo(0));
            }
            finally
            {
                SDL3.SDL_free(data);
            }
        }

        private static readonly Queue<string?> requestedMimeTypes = [];
        private static int cleanups;

        private const UIntPtr my_length = 17;

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe IntPtr dataCallback(IntPtr userdata, byte* mimeType, UIntPtr* length)
        {
            requestedMimeTypes.Enqueue(SDL3.PtrToStringUTF8(mimeType));

            *length = my_length;
            return IntPtr.Zero;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void cleanupCallback(IntPtr userdata)
        {
            cleanups++;
        }
    }
}
