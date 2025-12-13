// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using static SDL.SDL3_mixer;
using static SDL.SDL3;

namespace SDL.Tests
{
    [TestFixture]
    public class TestMixer
    {
        [Test]
        public unsafe void TestBasic()
        {
            SDL_Init(0);

            bool init = MIX_Init();

            try
            {
                Assert.That(init, Is.True, SDL_GetError);
                Assert.That(MIX_Version(), Is.EqualTo(SDL_MIXER_VERSION));

                Assume.That(MIX_GetNumAudioDecoders() > 0);
                string? name = MIX_GetAudioDecoder(0);
                Assert.That(name, Is.Not.Null, SDL_GetError);

                Assume.That(@"C:\Windows\Media\Windows Logon.wav", Does.Exist);
                var decoder = MIX_CreateAudioDecoder(@"C:\Windows\Media\Windows Logon.wav", 0);
                Assert.That(decoder != null, SDL_GetError);
                MIX_DestroyAudioDecoder(decoder);
            }
            finally
            {
                MIX_Quit();
                SDL_Quit();
            }
        }
    }
}
