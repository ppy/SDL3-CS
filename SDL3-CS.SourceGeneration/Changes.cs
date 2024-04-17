// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SDL3.SourceGeneration
{
    [Flags]
    public enum Changes
    {
        None,

        /// <summary>
        /// Change <c>const char*</c> function parameters to <see cref="Helper.Utf8StringStructName"/>.
        /// </summary>
        ChangeParamsToUtf8String = 1 << 0,

        /// <summary>
        /// Change <c>char *</c> or <c>const char *</c> return type to <see cref="string"/>.
        /// </summary>
        ChangeReturnTypeToString = 1 << 1,

        /// <summary>
        /// Call <c>SDL_free</c> on the returned pointer.
        /// </summary>
        FreeReturnedPointer = 1 << 2,

        /// <summary>
        /// Remove <see cref="Helper.UnsafePrefix"/> from method name.
        /// </summary>
        TrimUnsafeFromName = 1 << 3,
    }
}
