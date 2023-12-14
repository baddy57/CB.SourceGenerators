using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.DependencyProperty;

partial class DpTemplate
{
    public IEnumerable<UsingDirectiveSyntax> Usings { get; set; }
    public string NamespaceName { get; set; }
    public string ClassName { get; set; }

    public IEnumerable<DpField> DpFields { get; set; }
}
