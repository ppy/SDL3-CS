// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL
{
    using static SDL_PixelFormat;
    using static SDL_PixelType;
    using static SDL_PackedOrder;
    using static SDL_PackedLayout;
    using static SDL_MatrixCoefficients;
    using static SDL_ColorRange;

    public static partial class SDL3
    {
        [Macro]
        public static SDL_PixelFormat SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D) => (SDL_PixelFormat)SDL_FOURCC(A, B, C, D);

        [Macro]
        public static SDL_PixelFormat SDL_DEFINE_PIXELFORMAT(int type, int order, int layout, int bits, int bytes)
            => (SDL_PixelFormat)((1 << 28) | ((type) << 24) | ((order) << 20) | ((layout) << 16) |
                                 ((bits) << 8) | ((bytes) << 0));

        [Macro]
        public static int SDL_PIXELFLAG(SDL_PixelFormat X) => ((int)X >> 28) & 0x0F;

        [Macro]
        public static SDL_PixelType SDL_PIXELTYPE(SDL_PixelFormat X) => (SDL_PixelType)(((int)X >> 24) & 0x0F);

        [Macro]
        public static SDL_PackedOrder SDL_PIXELORDER(SDL_PixelFormat X) => (SDL_PackedOrder)(((int)X >> 20) & 0x0F);

        [Macro]
        public static SDL_PackedLayout SDL_PIXELLAYOUT(SDL_PixelFormat X) => (SDL_PackedLayout)(((int)X >> 16) & 0x0F);

        [Macro]
        public static int SDL_BITSPERPIXEL(SDL_PixelFormat X) => ((int)X >> 8) & 0xFF;

        [Macro]
        public static int SDL_BYTESPERPIXEL(SDL_PixelFormat X) =>
            (SDL_ISPIXELFORMAT_FOURCC(X)
                ? ((((X) == SDL_PIXELFORMAT_YUY2) ||
                    ((X) == SDL_PIXELFORMAT_UYVY) ||
                    ((X) == SDL_PIXELFORMAT_YVYU) ||
                    ((X) == SDL_PIXELFORMAT_P010))
                    ? 2
                    : 1)
                : ((((int)X) >> 0) & 0xFF));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_INDEXED(SDL_PixelFormat format) =>
            (!SDL_ISPIXELFORMAT_FOURCC(format) &&
             ((SDL_PIXELTYPE(format) == SDL_PIXELTYPE_INDEX1) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_INDEX2) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_INDEX4) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_INDEX8)));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_PACKED(SDL_PixelFormat format) =>
            (!SDL_ISPIXELFORMAT_FOURCC(format) &&
             ((SDL_PIXELTYPE(format) == SDL_PIXELTYPE_PACKED8) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_PACKED16) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_PACKED32)));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_ARRAY(SDL_PixelFormat format) =>
            (!SDL_ISPIXELFORMAT_FOURCC(format) &&
             ((SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYU8) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYU16) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYU32) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYF16) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYF32)));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_ALPHA(SDL_PixelFormat format) =>
            ((SDL_ISPIXELFORMAT_PACKED(format) &&
              ((SDL_PIXELORDER(format) == SDL_PACKEDORDER_ARGB) ||
               (SDL_PIXELORDER(format) == SDL_PACKEDORDER_RGBA) ||
               (SDL_PIXELORDER(format) == SDL_PACKEDORDER_ABGR) ||
               (SDL_PIXELORDER(format) == SDL_PACKEDORDER_BGRA))));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_10BIT(SDL_PixelFormat format) =>
            (!SDL_ISPIXELFORMAT_FOURCC(format) &&
             ((SDL_PIXELTYPE(format) == SDL_PIXELTYPE_PACKED32) &&
              (SDL_PIXELLAYOUT(format) == SDL_PACKEDLAYOUT_2101010)));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_FLOAT(SDL_PixelFormat format) =>
            (!SDL_ISPIXELFORMAT_FOURCC(format) &&
             ((SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYF16) ||
              (SDL_PIXELTYPE(format) == SDL_PIXELTYPE_ARRAYF32)));

        [Macro]
        public static bool SDL_ISPIXELFORMAT_FOURCC(SDL_PixelFormat format) =>
            ((format != 0) && (SDL_PIXELFLAG(format) != 1));

        [Macro]
        public static SDL_Colorspace SDL_DEFINE_COLORSPACE(UInt32 type, UInt32 range, UInt32 primaries, UInt32 transfer, UInt32 matrix, UInt32 chroma)
            => (SDL_Colorspace)(((type) << 28) | ((range) << 24) | ((chroma) << 20) |
                                ((primaries) << 10) | ((transfer) << 5) | ((matrix) << 0));

        [Macro]
        public static SDL_ColorType SDL_COLORSPACETYPE(SDL_Colorspace X) => (SDL_ColorType)(((int)X >> 28) & 0x0F);

        [Macro]
        public static SDL_ColorRange SDL_COLORSPACERANGE(SDL_Colorspace X) => (SDL_ColorRange)(((int)X >> 24) & 0x0F);

        [Macro]
        public static SDL_ChromaLocation SDL_COLORSPACECHROMA(SDL_Colorspace X) => (SDL_ChromaLocation)(((int)X >> 20) & 0x0F);

        [Macro]
        public static SDL_ColorPrimaries SDL_COLORSPACEPRIMARIES(SDL_Colorspace X) => (SDL_ColorPrimaries)(((int)X >> 10) & 0x1F);

        [Macro]
        public static SDL_TransferCharacteristics SDL_COLORSPACETRANSFER(SDL_Colorspace X) => (SDL_TransferCharacteristics)(((int)X >> 5) & 0x1F);

        [Macro]
        public static SDL_MatrixCoefficients SDL_COLORSPACEMATRIX(SDL_Colorspace X) => (SDL_MatrixCoefficients)((int)X & 0x1F);

        [Macro]
        public static bool SDL_ISCOLORSPACE_MATRIX_BT601(SDL_Colorspace X) => (SDL_COLORSPACEMATRIX(X) == SDL_MATRIX_COEFFICIENTS_BT601 || SDL_COLORSPACEMATRIX(X) == SDL_MATRIX_COEFFICIENTS_BT470BG);

        [Macro]
        public static bool SDL_ISCOLORSPACE_MATRIX_BT709(SDL_Colorspace X) => (SDL_COLORSPACEMATRIX(X) == SDL_MATRIX_COEFFICIENTS_BT709);

        [Macro]
        public static bool SDL_ISCOLORSPACE_MATRIX_BT2020_NCL(SDL_Colorspace X) => (SDL_COLORSPACEMATRIX(X) == SDL_MATRIX_COEFFICIENTS_BT2020_NCL);

        [Macro]
        public static bool SDL_ISCOLORSPACE_LIMITED_RANGE(SDL_Colorspace X) => (SDL_COLORSPACERANGE(X) != SDL_COLOR_RANGE_FULL);

        [Macro]
        public static bool SDL_ISCOLORSPACE_FULL_RANGE(SDL_Colorspace X) => (SDL_COLORSPACERANGE(X) == SDL_COLOR_RANGE_FULL);
    }
}
