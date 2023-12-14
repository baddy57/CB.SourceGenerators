using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.WpfInject;

partial class WpfInjectTemplate
{
    public IEnumerable<UsingDirectiveSyntax> Usings { get; set; }
    public string NamespaceName { get; set; }
    public string ClassName { get; set; }

    public IEnumerable<WpfInjectField> InjectFields { get; set; }
}
