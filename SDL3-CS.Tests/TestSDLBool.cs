// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestSDLBool
    {
        private SDL_bool invert(SDL_bool value) => value == SDL_bool.SDL_TRUE ? SDL_bool.SDL_FALSE : SDL_bool.SDL_TRUE;

        [Test]
        public void TestFalse()
        {
            Assert.That(SDL3.SDL_OutOfMemory(), Is.EqualTo(SDL_bool.SDL_FALSE));
        }

        [Test]
        public void TestTrue()
        {
            Assert.That(SDL3.SDL_ClearError(), Is.EqualTo(SDL_bool.SDL_TRUE));
        }

        [Test]
        public void TestStoreLoad([Values] SDL_bool value)
        {
            var props = SDL3.SDL_CreateProperties();
            Assume.That(props, Is.Not.EqualTo(0));

            var ret = SDL3.SDL_SetBooleanProperty(props, "test", value);
            Assume.That(ret, Is.EqualTo(SDL_bool.SDL_TRUE));

            Assert.That(SDL3.SDL_GetBooleanProperty(props, "test", invert(value)), Is.EqualTo(value));

            SDL3.SDL_DestroyProperties(props);
        }
    }
}
