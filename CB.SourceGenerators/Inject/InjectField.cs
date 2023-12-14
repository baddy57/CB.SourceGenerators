using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.Inject;

public class InjectField(string Type, string Name, bool IsOptional = false)
{
    public string ToParameterString() => IsOptional ? $"{Type} {Name} = null" : $"{Type} {Name}";

    public string Type { get; } = Type;
    public string Name { get; } = Name;
    public bool IsOptional { get; } = IsOptional;
}

public static class InjectFieldExtensions
{
    public static InjectField ToInjectField(this VariableDeclaratorSyntax syntax, bool optional)
    {
        var type = syntax.FirstAncestorOrSelf<FieldDeclarationSyntax>().Declaration.Type.ToString();
        var name = syntax.Identifier.ValueText;
        return new InjectField(type, name, optional);
    }
}