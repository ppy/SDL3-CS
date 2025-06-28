// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SDL.SourceGeneration
{
    public class UnfriendlyMethodFinder : ISyntaxReceiver
    {
        public readonly Dictionary<string, List<GeneratedMethod>> Methods = new Dictionary<string, List<GeneratedMethod>>();

        private static readonly string[] sdlPrefixes = ["SDL_", "TTF_", "IMG_", "Mix_"];

        /// <summary>
        /// Checks whether the method is from any SDL library.
        /// It identifies those by checking the SDL prefix in the method name.
        /// </summary>
        private static bool IsMethodFromSDL(MethodDeclarationSyntax methodNode)
        {
            foreach (string prefix in sdlPrefixes)
            {
                if (methodNode.Identifier.ValueText.StartsWith(prefix, StringComparison.Ordinal))
                    return true;
            }

            return false;
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is MethodDeclarationSyntax method)
            {
                string name = method.Identifier.ValueText;
                bool isUnsafe = name.StartsWith(Helper.UnsafePrefix, StringComparison.Ordinal);

                if (!IsMethodFromSDL(method) && !isUnsafe)
                    return;

                if (method.ParameterList.Parameters.Any(p => p.Identifier.IsKind(SyntaxKind.ArgListKeyword)))
                    return;

                var changes = Changes.None;

                // if the method is not marked unsafe, the `byte*` is not a string.
                if (method.ReturnType.IsBytePtr() && isUnsafe)
                {
                    changes |= Changes.ChangeReturnTypeToString | Changes.TrimUnsafeFromName;

                    if (!method.IsReturnTypeConstCharPtr())
                        changes |= Changes.FreeReturnedPointer;
                }

                foreach (var parameter in method.ParameterList.Parameters)
                {
                    if (parameter.IsTypeConstCharPtr())
                        changes |= Changes.ChangeParamsToUtf8String;
                }

                if (changes != Changes.None)
                {
                    string fileName = Path.GetFileName(method.SyntaxTree.FilePath);

                    if (!Methods.TryGetValue(fileName, out var list))
                        Methods[fileName] = list = [];

                    list.Add(new GeneratedMethod(method, changes));
                }
            }
        }
    }
}
