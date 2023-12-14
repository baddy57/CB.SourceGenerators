using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.DependencyProperty;

public class DpField(string type, string name, string defaultValue = "null")
{
    public string Type { get; } = type;
    public string Name { get; } = name.ToCamelCase();
    public string DefaultValue { get; } = defaultValue;
}

public static class DpFieldExtensions
{
    public static DpField ToDpField(this VariableDeclaratorSyntax syntax, GeneratorExecutionContext context)
    {
        var type = syntax.FirstAncestorOrSelf<FieldDeclarationSyntax>().Declaration.Type.ToString();
        var name = syntax.Identifier.ValueText;

        var initializer = syntax.FirstAncestorOrSelf<FieldDeclarationSyntax>().Declaration.Variables
            .First().Initializer;
        string value = "null";

        // If the property/field has an initializer
        if (initializer is not null)
        {
            value = initializer.Value.ToString();
        }

        Console.WriteLine(value);

        return new DpField(type, name, value/*value?.ToString()*/);
    }

    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        while (input.StartsWith("_"))
            input = input.Substring(1);

        // Split the string into words
        string[] words = input.Split('_');

        for (int i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
        }

        return string.Concat(words);
    }
}
