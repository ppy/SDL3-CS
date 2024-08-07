/*
  <auto-generated/>
  C# bindings for Simple DirectMedia Layer.
  Original copyright notice of input files:

  Simple DirectMedia Layer
  Copyright (C) 1997-2024 Sam Lantinga <slouken@libsdl.org>

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.
*/

using System.Runtime.InteropServices;

namespace SDL
{
    public enum SDL_BlendOperation
    {
        SDL_BLENDOPERATION_ADD = 0x1,
        SDL_BLENDOPERATION_SUBTRACT = 0x2,
        SDL_BLENDOPERATION_REV_SUBTRACT = 0x3,
        SDL_BLENDOPERATION_MINIMUM = 0x4,
        SDL_BLENDOPERATION_MAXIMUM = 0x5,
    }

    public enum SDL_BlendFactor
    {
        SDL_BLENDFACTOR_ZERO = 0x1,
        SDL_BLENDFACTOR_ONE = 0x2,
        SDL_BLENDFACTOR_SRC_COLOR = 0x3,
        SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 0x4,
        SDL_BLENDFACTOR_SRC_ALPHA = 0x5,
        SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 0x6,
        SDL_BLENDFACTOR_DST_COLOR = 0x7,
        SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR = 0x8,
        SDL_BLENDFACTOR_DST_ALPHA = 0x9,
        SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 0xA,
    }

    public static partial class SDL3
    {
        [DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SDL_BlendMode SDL_ComposeCustomBlendMode(SDL_BlendFactor srcColorFactor, SDL_BlendFactor dstColorFactor, SDL_BlendOperation colorOperation, SDL_BlendFactor srcAlphaFactor, SDL_BlendFactor dstAlphaFactor, SDL_BlendOperation alphaOperation);

        [NativeTypeName("#define SDL_BLENDMODE_NONE 0x00000000u")]
        public const uint SDL_BLENDMODE_NONE = 0x00000000U;

        [NativeTypeName("#define SDL_BLENDMODE_BLEND 0x00000001u")]
        public const uint SDL_BLENDMODE_BLEND = 0x00000001U;

        [NativeTypeName("#define SDL_BLENDMODE_BLEND_PREMULTIPLIED 0x00000010u")]
        public const uint SDL_BLENDMODE_BLEND_PREMULTIPLIED = 0x00000010U;

        [NativeTypeName("#define SDL_BLENDMODE_ADD 0x00000002u")]
        public const uint SDL_BLENDMODE_ADD = 0x00000002U;

        [NativeTypeName("#define SDL_BLENDMODE_ADD_PREMULTIPLIED 0x00000020u")]
        public const uint SDL_BLENDMODE_ADD_PREMULTIPLIED = 0x00000020U;

        [NativeTypeName("#define SDL_BLENDMODE_MOD 0x00000004u")]
        public const uint SDL_BLENDMODE_MOD = 0x00000004U;

        [NativeTypeName("#define SDL_BLENDMODE_MUL 0x00000008u")]
        public const uint SDL_BLENDMODE_MUL = 0x00000008U;

        [NativeTypeName("#define SDL_BLENDMODE_INVALID 0x7FFFFFFFu")]
        public const uint SDL_BLENDMODE_INVALID = 0x7FFFFFFFU;
    }
}
