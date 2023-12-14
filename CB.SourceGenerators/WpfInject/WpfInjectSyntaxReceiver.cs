using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.WpfInject;

// A syntax receiver that will collect the candidate classes that have fields with the [Inject] attribute
public class WpfInjectSyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = [];

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // Check if the syntax node is a class declaration that has fields with the [Inject] attribute
        if (syntaxNode is ClassDeclarationSyntax classDeclaration
            && classDeclaration.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() is "WpfInject"))
            && classDeclaration.Members.OfType<FieldDeclarationSyntax>()
            .Any(f => f.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() is "Inject" or "InjectOptional"))))
        {
            // Add the class declaration to the candidate list
            CandidateClasses.Add(classDeclaration);
        }
    }
}
