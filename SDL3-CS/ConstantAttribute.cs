// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;

namespace SDL
{
    /// <summary>
    /// Denotes a manually defined constant.
    /// Such consants should be excluded from ClangSharp generation to prevent warnings or duplicate definitions.
    /// Handled by <c>get_manually_written_symbols()</c> in generate_bindings.py.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("NEVER")]
    public class ConstantAttribute : Attribute;
}
