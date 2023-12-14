using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CB.SourceGenerators.Inject;

partial class DIConstructorTemplate
{
    public IEnumerable<UsingDirectiveSyntax> Usings { get; set; }
    public string NamespaceName { get; set; }
    public string ClassName { get; set; }

    public IEnumerable<InjectField> InjectFields { get; set; }
}
