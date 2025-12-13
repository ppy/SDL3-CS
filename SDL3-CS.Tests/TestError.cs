// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestError
    {
        [Test]
        public void TestUnsupported()
        {
            Assert.That((bool)SDL3.SDL_Unsupported(), Is.False);
            Assert.That(SDL3.SDL_GetError(), Is.EqualTo("That operation is not supported"));
        }

        [Test]
        public void TestInvalidParam()
        {
            Assert.That((bool)SDL3.SDL_InvalidParamError("test"), Is.False);
            Assert.That(SDL3.SDL_GetError(), Is.EqualTo("Parameter 'test' is invalid"));
        }
    }
}
