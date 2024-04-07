// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;

namespace SDL
{
    /// <summary>
    /// Denotes a manually implemented macro function.
    /// Such functions should be excluded from ClangSharp generation to prevent warnings.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [Conditional("NEVER")]
    internal class MacroAttribute : Attribute;
}
