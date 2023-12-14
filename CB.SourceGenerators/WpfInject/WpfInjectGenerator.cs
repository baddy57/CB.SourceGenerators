using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CB.SourceGenerators.WpfInject;

[Generator]
public class WpfInjectGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new WpfInjectSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var receiver = (WpfInjectSyntaxReceiver)context.SyntaxReceiver;

        // Loop through the candidate classes that have fields with the [Inject] attribute
        foreach (var classDeclaration in receiver.CandidateClasses)
        {
            var className = classDeclaration.Identifier.ValueText;

            var namespaceName = classDeclaration.FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name.ToString()
                ?? classDeclaration.FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>()?.Name.ToString();

            // [Inject]
            var injectFields = classDeclaration.Members.OfType<FieldDeclarationSyntax>()
                .Where(f => f.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() == "Inject")))
                .SelectMany(f => f.Declaration.Variables);

            // [InjectOptional]
            var injectOptionalFields = classDeclaration.Members.OfType<FieldDeclarationSyntax>()
                .Where(f => f.AttributeLists.Any(a => a.Attributes.Any(at => at.Name.ToString() == "InjectOptional")))
                .SelectMany(f => f.Declaration.Variables);

            var allFields = injectFields.Select(x => x.ToWpfInjectField(false))
                .Union(injectOptionalFields.Select(x => x.ToWpfInjectField(true)));

            var usings = classDeclaration.GetUsingDirectives();

            var source = new WpfInjectTemplate
            {
                Usings = usings,
                NamespaceName = namespaceName,
                ClassName = className,
                InjectFields = allFields,
            }.TransformText();

            context.AddSource($"{className}_Inject.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
