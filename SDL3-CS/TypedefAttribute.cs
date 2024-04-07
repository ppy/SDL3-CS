// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;

namespace SDL
{
    /// <summary>
    /// Denotes a C <c>typedef</c> that is expressed as an enum in C#.
    /// These need to be manually signaled to ClangSharp so it uses the custom enum type instead of the underlying type.
    /// Handled by <c>get_typedefs()</c> in generate_bindings.py.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    [Conditional("NEVER")]
    public class TypedefAttribute : Attribute;
}
