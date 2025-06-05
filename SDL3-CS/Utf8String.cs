// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Text;

namespace SDL
{
    /// <summary>
    /// Null pointer or a null-byte terminated UTF8 string suitable for use in native methods.
    /// </summary>
    /// <remarks>Should only be instantiated through implicit conversions or with <c>null</c>.</remarks>
    public readonly ref struct Utf8String
    {
        internal readonly ReadOnlySpan<byte> Raw;

        private Utf8String(ReadOnlySpan<byte> raw)
        {
            Raw = raw;
        }

        public static implicit operator Utf8String(string? str)
        {
            if (str == null)
                return new Utf8String(null);

            if (str.Length == 0)
                return new Utf8String("\0"u8);

            if (str[^1] == '\0')
                return new Utf8String(Encoding.UTF8.GetBytes(str));

            int len = Encoding.UTF8.GetByteCount(str);
            byte[] bytes = new byte[len + 1];
            Encoding.UTF8.GetBytes(str, bytes);
            return new Utf8String(bytes);
        }

        public static implicit operator Utf8String(ReadOnlySpan<byte> raw)
        {
            if (raw == null)
                return new Utf8String(null);

            if (raw.Length == 0)
                return new Utf8String("\0"u8);

            if (raw[^1] == 0)
                return new Utf8String(raw);

            byte[] copy = new byte[raw.Length + 1];
            raw.CopyTo(copy);
            return new Utf8String(copy);
        }

        internal ref readonly byte GetPinnableReference() => ref Raw.GetPinnableReference();
    }
}
