using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CB.SourceGenerators.DependencyProperty;

[Generator]
public class DependencyPropertyGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new DepPropSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var receiver = (DepPropSyntaxReceiver)context.SyntaxReceiver;

        // Loop through the candidate classes that have fields with the [Inject] attribute
        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            var className = classDeclaration.Identifier.ValueText;

            var namespaceName = classDeclaration.FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name.ToString()
                ?? classDeclaration.FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>()?.Name.ToString();

            // [DependencyProperty]
            var dpFields = classDeclaration.Members.OfType<FieldDeclarationSyntax>()
                .Where(f => f.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() == "DependencyProperty")))
                .SelectMany(f => f.Declaration.Variables)
                .Select(x => x.ToDpField(context));

            var usings = classDeclaration.GetUsingDirectives();

            var source = new DpTemplate
            {
                Usings = usings,
                NamespaceName = namespaceName,
                ClassName = className,
                DpFields = dpFields,
            }.TransformText();

            context.AddSource($"{className}_DependencyProperties.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
