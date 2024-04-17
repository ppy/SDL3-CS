// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace SDL
{
    /// <summary>
    /// Null pointer or a null-byte terminated UTF8 string suitable for use in native methods.
    /// </summary>
    /// <remarks>Should only be instantiated through implicit conversions.</remarks>
    public readonly ref struct Utf8String
    {
        public readonly ReadOnlySpan<byte> Raw;

        private Utf8String(ReadOnlySpan<byte> raw)
        {
            Raw = raw;
        }

        public static implicit operator Utf8String(string? str)
        {
            if (str == null)
                return new Utf8String(null);

            return new Utf8String(Encoding.UTF8.GetBytes(ensureTrailingNull(str)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ensureTrailingNull(string str) => str.EndsWith('\0') ? str : str + '\0';

        public static implicit operator Utf8String(ReadOnlySpan<byte> raw)
        {
            if (raw == null)
                return new Utf8String(null);

            if (raw.Length == 0)
                return new Utf8String(new ReadOnlySpan<byte>([0]));

            if (raw[^1] != 0)
            {
                byte[] copy = new byte[raw.Length + 1];
                raw.CopyTo(copy);
                raw = copy;
            }

            return new Utf8String(raw);
        }

        internal ref readonly byte GetPinnableReference() => ref Raw.GetPinnableReference();
    }
}
