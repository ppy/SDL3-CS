// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SDL.SourceGeneration
{
    public record GeneratedMethod
    {
        public readonly MethodDeclarationSyntax NativeMethod;
        public readonly Changes RequiredChanges;

        public GeneratedMethod(MethodDeclarationSyntax nativeMethod, Changes requiredChanges)
        {
            NativeMethod = nativeMethod;
            RequiredChanges = requiredChanges;
        }
    }
}
