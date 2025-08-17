// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

// https://github.com/JetBrains/JetBrains.Annotations/blob/988289d09aad925b22a5321c075a735cb9597e59/src/Annotations.cs
namespace JetBrains.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Parameter)]
    internal sealed class MustDisposeResourceAttribute : Attribute
    {
        public MustDisposeResourceAttribute()
        {
            Value = true;
        }

        public MustDisposeResourceAttribute(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// When set to <c>false</c>, disposing of the resource is not obligatory.
        /// The main use-case for explicit <c>[MustDisposeResource(false)]</c> annotation
        /// is to loosen the annotation for inheritors.
        /// </summary>
        public bool Value { get; }
    }
}
