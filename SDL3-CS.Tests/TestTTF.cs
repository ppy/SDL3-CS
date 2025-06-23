// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using static SDL.SDL3_ttf;
using static SDL.SDL3;

namespace SDL.Tests
{
    public unsafe class TestTTF
    {
        [Test]
        public void TestBasic()
        {
            SDL_Init(0);

            bool init = TTF_Init();

            try
            {
                Assert.That(init, Is.True, SDL_GetError);
                Assert.That(TTF_Version(), Is.EqualTo(SDL_TTF_VERSION));

                Assume.That(@"C:\Windows\Fonts\times.ttf", Does.Exist);
                var font = TTF_OpenFont(@"C:\Windows\Fonts\times.ttf", 12f);

                try
                {
                    Assert.That(font != null, SDL_GetError);
                    string? name = PtrToStringUTF8(TTF_GetFontFamilyName(font)); // TODO: fix Unsafe_ generation
                    Assert.That(name, Is.EqualTo("Times New Roman"));
                }
                finally
                {
                    TTF_CloseFont(font);
                }
            }
            finally
            {
                TTF_Quit();
                SDL_Quit();
            }
        }
    }
}
