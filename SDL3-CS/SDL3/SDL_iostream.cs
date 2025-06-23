// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
#pragma warning disable CS0618 // Type or member is obsolete
    public partial struct SDL_IOStreamInterface : SDL3.ISDLInterface
#pragma warning restore CS0618 // Type or member is obsolete
    {
        uint SDL3.ISDLInterface.version { set => version = value; }
    }
}
