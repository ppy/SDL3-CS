// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Text;

namespace SDL
{
    public readonly record struct SDLBool
    {
        private readonly byte value;

        internal const byte FALSE_VALUE = 0;
        internal const byte TRUE_VALUE = 1;

        [Obsolete("Never explicitly construct an SDL bool.")]
        public SDLBool()
        {
        }

        internal SDLBool(byte value)
        {
            this.value = value;
        }

        public static implicit operator bool(SDLBool b) => b.value != FALSE_VALUE;

        public static implicit operator SDLBool(bool b) => new SDLBool(b ? TRUE_VALUE : FALSE_VALUE);

        public bool Equals(SDLBool other) => (bool)other == (bool)this;

        public override int GetHashCode() => ((bool)this).GetHashCode();

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"0x{value:x2}");
            return true;
        }
    }

    [Typedef]
    public enum SDL_Time : Int64;

    public partial class SDL3
    {
        [Macro]
        public static uint SDL_FOURCC(byte A, byte B, byte C, byte D) => (uint)((A << 0) | (B << 8) | (C << 16) | (D << 24));

        [Obsolete("Do not use.")] // used internally
        public interface ISDLInterface
        {
            internal uint version { set; }
        }

        [Macro]
        public static unsafe void SDL_INIT_INTERFACE<T>(out T iface)
#pragma warning disable CS0618 // Type or member is obsolete
            where T : unmanaged, ISDLInterface
#pragma warning restore CS0618 // Type or member is obsolete
        {
            iface = default;
            iface.version = (uint)sizeof(T);
        }

        public static unsafe void SDL_free(void* mem) => SDL_free((IntPtr)mem);
    }
}
