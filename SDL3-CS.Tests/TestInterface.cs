// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;

namespace SDL.Tests
{
    [TestFixture]
    public class TestInterface
    {
        [Test]
        public unsafe void TestIOInterface()
        {
            SDL_IOStreamInterface iface;

            SDL3.SDL_INIT_INTERFACE(out iface);

            Assert.That(iface.version, Is.EqualTo(sizeof(SDL_IOStreamInterface)));
            Assert.That(iface.size == null);
            Assert.That(iface.seek == null);
            Assert.That(iface.read == null);
            Assert.That(iface.write == null);
            Assert.That(iface.flush == null);
            Assert.That(iface.close == null);
        }

        [Test]
        public unsafe void TestIOInterfaceManual()
        {
            SDL_IOStreamInterface iface = new SDL_IOStreamInterface
            {
                version = (uint)sizeof(SDL_IOStreamInterface),
            };

            Assert.That(iface.version, Is.EqualTo(sizeof(SDL_IOStreamInterface)));
            Assert.That(iface.size == null);
            Assert.That(iface.seek == null);
            Assert.That(iface.read == null);
            Assert.That(iface.write == null);
            Assert.That(iface.flush == null);
            Assert.That(iface.close == null);
        }
    }
}
