using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.DependencyProperty;

// A syntax receiver that will collect the candidate classes that have fields with the [DependencyProperty] attribute
public class DepPropSyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = [];

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // Check if the syntax node is a class declaration that has fields with the [DependencyProperty] attribute
        if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
            classDeclaration.Members.OfType<FieldDeclarationSyntax>()
            .Any(f => f.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() is "DependencyProperty"))))
        {
            // Add the class declaration to the candidate list
            CandidateClasses.Add(classDeclaration);
        }
    }
}
