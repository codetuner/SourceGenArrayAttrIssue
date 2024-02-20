using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CloneSourceGenerator
{
    [Generator]
    public class CloneGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        { }

        public void Execute(GeneratorExecutionContext context)
        {
            using (var writer = new StringWriter())
            {
                foreach (var clone in GetClonesToGenerate(context, writer))
                {
                    var typeSymbol = context.Compilation.GetTypeByMetadataName(clone.Item1);
                    writer.WriteLine($"public class {clone.Item2}");
                    writer.WriteLine("{");
                    foreach (var prop in typeSymbol.GetMembers().OfType<IPropertySymbol>())
                    {
                        foreach (var attr in prop.GetAttributes())
                        {
                            writer.WriteLine($"\t[{attr.ToString()}]");
                        }
                        writer.WriteLine($"\tpublic {prop.Type} {prop.Name} {{ get; set; }}");
                        writer.WriteLine();
                    }

                    writer.WriteLine("}");
                    writer.WriteLine();
                }

                context.AddSource("clones.g", SourceText.From(writer.ToString(), Encoding.UTF8));
            }
        }

        private IEnumerable<Tuple<string, string>> GetClonesToGenerate(GeneratorExecutionContext context, TextWriter writer)
        {
            var classes = context.Compilation.SyntaxTrees.SelectMany(t => t.GetRoot().DescendantNodes())
                .Where(n => n is ClassDeclarationSyntax)
                .Cast<ClassDeclarationSyntax>();

            var result = new List<Tuple<string, string>>();
            foreach (var cl in classes)
            {
                foreach (var al in cl.AttributeLists)
                {
                    foreach (var at in al.Attributes.Where(at => at.Name.ToString() == "UseCloneFor"))
                    {
                        yield return new Tuple<string, string>(at.ArgumentList.Arguments[0].ToString().Replace("\"", ""), at.ArgumentList.Arguments[1].ToString().Replace("\"", ""));
                    }
                }
            }
        }
    }
}
