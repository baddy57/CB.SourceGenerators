using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators;

// A helper extension method to get the using directives from a class declaration
public static class InjectSyntaxExtensions
{
    public static List<UsingDirectiveSyntax> GetUsingDirectives(this ClassDeclarationSyntax classDeclaration)
    {
        var usingDirectives = new List<UsingDirectiveSyntax>();

        // Get the using directives from the compilation unit
        var compilationUnit = classDeclaration.FirstAncestorOrSelf<CompilationUnitSyntax>();
        if (compilationUnit != null)
        {
            usingDirectives.AddRange(compilationUnit.Usings);
        }

        // Get the using directives from the namespace declaration
        var namespaceDeclaration = classDeclaration.FirstAncestorOrSelf<NamespaceDeclarationSyntax>();
        if (namespaceDeclaration != null)
        {
            usingDirectives.AddRange(namespaceDeclaration.Usings);
        }

        return usingDirectives;
    }
}