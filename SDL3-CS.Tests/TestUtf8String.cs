// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using SDL;

namespace SDL3.Tests
{
    [TestFixture]
    public class TestUtf8String
    {
        [Test]
        public void TestNoImplicitConversion()
        {
            checkNull(null);
            checkNull(default);
            checkNull(new Utf8String()); // don't do this in actual code
        }

        [TestCase(null, -1)]
        [TestCase("", 1)]
        [TestCase("\0", 1)]
        [TestCase("test", 5)]
        [TestCase("test\0", 5)]
        [TestCase("test\0test", 10)]
        [TestCase("test\0test\0", 10)]
        public static void TestString(string? str, int expectedLength)
        {
            if (str == null)
                checkNull(str);
            else
                check(str, expectedLength);
        }

        [Test]
        public static void TestNullSpan()
        {
            ReadOnlySpan<byte> span = null;
            checkNull(span);
        }

        [Test]
        public static void TestDefaultSpan()
        {
            ReadOnlySpan<byte> span = default;
            checkNull(span);
        }

        [Test]
        public static void TestNewSpan()
        {
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>();
            checkNull(span);
        }

        [Test]
        public static void TestReadOnlySpan()
        {
            check(""u8, 1);
            check("\0"u8, 1);
            check("test"u8, 5);
            check("test\0"u8, 5);
            check("test\0test"u8, 10);
            check("test\0test\0"u8, 10);
        }

        private static unsafe void checkNull(Utf8String s)
        {
            Assert.That(s.Raw == null, "s.Raw == null");
            Assert.That(s.Raw.Length, Is.EqualTo(0));

            fixed (byte* ptr = s.Raw)
            {
                Assert.That(ptr == null, "ptr == null");
            }
        }

        private static unsafe void check(Utf8String s, int expectedLength)
        {
            Assert.That(s.Raw.Length, Is.EqualTo(expectedLength));

            fixed (byte* ptr = s.Raw)
            {
                Assert.That(ptr != null, "ptr != null");
                Assert.That(ptr[s.Raw.Length - 1], Is.EqualTo(0));
            }
        }
    }
}
