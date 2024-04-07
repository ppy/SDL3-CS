// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SDL
{
    public static partial class SDL3
    {
        [Macro]
        public static int SDL_OutOfMemory() => SDL_Error(SDL_errorcode.SDL_ENOMEM);

        [Macro]
        public static int SDL_Unsupported() => SDL_Error(SDL_errorcode.SDL_UNSUPPORTED);

        [Macro]
        public static unsafe int SDL_InvalidParamError([NativeTypeName("const char *")] byte* param)
        {
            fixed (byte* fmt = "Parameter '%s' is invalid"u8)
                return SDL_SetError(fmt, __arglist(param));
        }
    }
}
