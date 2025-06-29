// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SDL3-CS.Tests")]

// Allow access to internal CodeGen members (e.g. NativeTypeNameAttribute, etc.) for SDL sister projects:
[assembly: InternalsVisibleTo("SDL3_ttf-CS")]
[assembly: InternalsVisibleTo("SDL3_image-CS")]
[assembly: InternalsVisibleTo("SDL3_mixer-CS")]
