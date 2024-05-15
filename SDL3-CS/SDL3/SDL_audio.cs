// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace SDL
{
    [Typedef]
    public enum SDL_AudioDeviceID : UInt32;

    [Typedef]
    public enum SDL_AudioFormat : UInt16
    {
        SDL_AUDIO_U8 = (UInt16)SDL3.SDL_AUDIO_U8,
        SDL_AUDIO_S8 = (UInt16)SDL3.SDL_AUDIO_S8,
        SDL_AUDIO_S16LE = (UInt16)SDL3.SDL_AUDIO_S16LE,
        SDL_AUDIO_S16BE = (UInt16)SDL3.SDL_AUDIO_S16BE,
        SDL_AUDIO_S32LE = (UInt16)SDL3.SDL_AUDIO_S32LE,
        SDL_AUDIO_S32BE = (UInt16)SDL3.SDL_AUDIO_S32BE,
        SDL_AUDIO_F32LE = (UInt16)SDL3.SDL_AUDIO_F32LE,
        SDL_AUDIO_F32BE = (UInt16)SDL3.SDL_AUDIO_F32BE,
    }

    public static partial class SDL3
    {
        [Constant]
        public static readonly SDL_AudioFormat SDL_AUDIO_S16 = BitConverter.IsLittleEndian ? SDL_AudioFormat.SDL_AUDIO_S16LE : SDL_AudioFormat.SDL_AUDIO_S16BE;

        [Constant]
        public static readonly SDL_AudioFormat SDL_AUDIO_S32 = BitConverter.IsLittleEndian ? SDL_AudioFormat.SDL_AUDIO_S32LE : SDL_AudioFormat.SDL_AUDIO_S32BE;

        [Constant]
        public static readonly SDL_AudioFormat SDL_AUDIO_F32 = BitConverter.IsLittleEndian ? SDL_AudioFormat.SDL_AUDIO_F32LE : SDL_AudioFormat.SDL_AUDIO_F32BE;

        [Macro]
        public static int SDL_AUDIO_BITSIZE(SDL_AudioFormat x) => (int)((UInt16)x & SDL_AUDIO_MASK_BITSIZE);

        [Macro]
        public static int SDL_AUDIO_BYTESIZE(SDL_AudioFormat x) => SDL_AUDIO_BITSIZE(x) / 8;

        [Macro]
        public static bool SDL_AUDIO_ISFLOAT(SDL_AudioFormat x) => ((UInt16)x & SDL_AUDIO_MASK_FLOAT) != 0;

        [Macro]
        public static bool SDL_AUDIO_ISBIGENDIAN(SDL_AudioFormat x) => ((UInt16)x & SDL_AUDIO_MASK_BIG_ENDIAN) != 0;

        [Macro]
        public static bool SDL_AUDIO_ISLITTLEENDIAN(SDL_AudioFormat x) => !SDL_AUDIO_ISBIGENDIAN(x);

        [Macro]
        public static bool SDL_AUDIO_ISSIGNED(SDL_AudioFormat x) => ((UInt16)x & SDL_AUDIO_MASK_SIGNED) != 0;

        [Macro]
        public static bool SDL_AUDIO_ISINT(SDL_AudioFormat x) => !SDL_AUDIO_ISFLOAT(x);

        [Macro]
        public static bool SDL_AUDIO_ISUNSIGNED(SDL_AudioFormat x) => !SDL_AUDIO_ISSIGNED(x);

        [Macro]
        public static int SDL_AUDIO_FRAMESIZE(SDL_AudioSpec x) => SDL_AUDIO_BYTESIZE((x).format) * (x).channels;

        [MustDisposeResource]
        public static unsafe SDLArray<SDL_AudioDeviceID>? SDL_GetAudioOutputDevices()
        {
            int count;
            var array = SDL_GetAudioOutputDevices(&count);
            return SDLArray.Create(array, count);
        }

        [MustDisposeResource]
        public static unsafe SDLArray<SDL_AudioDeviceID>? SDL_GetAudioCaptureDevices()
        {
            int count;
            var array = SDL_GetAudioCaptureDevices(&count);
            return SDLArray.Create(array, count);
        }
    }
}
