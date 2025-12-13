// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static unsafe SDLBool SDL_Unsupported()
        {
            fixed (byte* fmt = "That operation is not supported\0"u8)
                return SDL_SetError(fmt, __arglist());
        }

        [Macro]
        public static unsafe SDLBool SDL_InvalidParamError([NativeTypeName("const char *")] byte* param)
        {
            fixed (byte* fmt = "Parameter '%s' is invalid\0"u8)
                return SDL_SetError(fmt, __arglist(param));
        }
    }
}
