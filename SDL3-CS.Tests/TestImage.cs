// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using static SDL.SDL3_image;
using static SDL.SDL3;

namespace SDL.Tests
{
    public unsafe class TestImage
    {
        [Test]
        public void TestBasic()
        {
            SDL_Init(0);

            try
            {
                Assert.That(IMG_Version(), Is.EqualTo(SDL_IMAGE_VERSION));

                Assume.That("sample.png", Does.Exist);
                var image = IMG_Load("sample.png");

                try
                {
                    Assert.That(image != null, SDL_GetError);
                    Assert.That(image->w, Is.EqualTo(23));
                    Assert.That(image->h, Is.EqualTo(42));
                }
                finally
                {
                    SDL_DestroySurface(image);
                }
            }
            finally
            {
                SDL_Quit();
            }
        }
    }
}
