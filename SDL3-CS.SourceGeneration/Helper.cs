// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SDL3.SourceGeneration
{
    public static class Helper
    {
        /// <remarks>
        /// Needs to match <c>unsafe_prefix</c> in generate_bindings.py.
        /// </remarks>
        public const string UnsafePrefix = "Unsafe_";

        public static bool IsVoid(this TypeSyntax type) => type is PredefinedTypeSyntax predefined
                                                           && predefined.Keyword.IsKind(SyntaxKind.VoidKeyword);

        public static bool IsBytePtr(this TypeSyntax? type) => type is PointerTypeSyntax syntax
                                                               && syntax.ElementType is PredefinedTypeSyntax predefined
                                                               && predefined.Keyword.IsKind(SyntaxKind.ByteKeyword);

        public static IEnumerable<AttributeSyntax> GetReturnAttributes(this MethodDeclarationSyntax method)
        {
            foreach (var list in method.AttributeLists)
            {
                if (list.Target?.Identifier.IsKind(SyntaxKind.ReturnKeyword) == true)
                {
                    foreach (var attribute in list.Attributes)
                        yield return attribute;
                }
            }
        }

        public static IEnumerable<AttributeSyntax> GetAttributes(this ParameterSyntax parameter)
        {
            foreach (var list in parameter.AttributeLists)
            {
                foreach (var attribute in list.Attributes)
                    yield return attribute;
            }
        }

        public static bool IsConstCharPtr(this AttributeSyntax attribute)
        {
            if (attribute.Name is IdentifierNameSyntax identifier
                && identifier.Identifier.ValueText == "NativeTypeName"
                && attribute.ArgumentList != null)
            {
                return attribute.ArgumentList.Arguments.Any(a => a.Expression is LiteralExpressionSyntax literal
                                                                 && isConstCharPtr(literal.Token.ValueText));
            }

            return false;
        }

        private static bool isConstCharPtr(string text)
        {
            switch (text)
            {
                case "const char*":
                case "const char *":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsReturnTypeConstCharPtr(this MethodDeclarationSyntax method) => method.GetReturnAttributes().Any(IsConstCharPtr);

        public static bool IsTypeConstCharPtr(this ParameterSyntax parameter) => parameter.GetAttributes().Any(IsConstCharPtr);

        public static InvocationExpressionSyntax WithArguments(this InvocationExpressionSyntax invocationExpression, IEnumerable<ArgumentSyntax> arguments)
            => invocationExpression.WithArgumentList(SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments)));
    }
}
