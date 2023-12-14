using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.WpfInject;

public class WpfInjectField(string Type, string Name, bool IsOptional = false)
{
    public string ToIocString() => IsOptional
        ? $"CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.GetService<{Type}>()"
        : $"CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.GetRequiredService<{Type}>()";

    public string Type { get; } = Type;
    public string Name { get; } = Name;
    public bool IsOptional { get; } = IsOptional;
}

public static class WpfInjectFieldExtensions
{
    public static WpfInjectField ToWpfInjectField(this VariableDeclaratorSyntax syntax, bool optional)
    {
        var type = syntax.FirstAncestorOrSelf<FieldDeclarationSyntax>().Declaration.Type.ToString();
        var name = syntax.Identifier.ValueText;
        return new WpfInjectField(type, name, optional);
    }
}