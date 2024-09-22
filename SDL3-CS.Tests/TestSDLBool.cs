// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections;
using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestSDLBool
    {
        [Test]
        public void TestFalse()
        {
            Assert.That((bool)SDL3.SDL_OutOfMemory(), Is.EqualTo(false));
        }

        [Test]
        public void TestTrue()
        {
            Assert.That((bool)SDL3.SDL_ClearError(), Is.EqualTo(true));
        }

        [Test]
        public void TestStoreLoad([Values] bool value)
        {
            var props = SDL3.SDL_CreateProperties();
            Assume.That(props, Is.Not.EqualTo(0));

            bool ret = SDL3.SDL_SetBooleanProperty(props, "test", value);
            Assume.That(ret, Is.EqualTo(true));

            Assert.That((bool)SDL3.SDL_GetBooleanProperty(props, "test", !value), Is.EqualTo(value));

            SDL3.SDL_DestroyProperties(props);
        }

        public static IEnumerable BoolCases()
        {
            SDLBool[] values = [new SDLBool(4), true, false];

            foreach (var x in values)
            {
                foreach (var y in values)
                {
                    yield return new object[] { x, y };
                }
            }
        }

        [TestCaseSource(typeof(TestSDLBool), nameof(BoolCases))]
        public void TestEquals(SDLBool first, SDLBool second)
        {
            Assert.That((SDLBool)(bool)first, Is.EqualTo(first));

            Assert.That(first.Equals(second), Is.EqualTo((bool)first == (bool)second));
            Assert.That(first == second, Is.EqualTo((bool)first == (bool)second));
        }
    }
}
