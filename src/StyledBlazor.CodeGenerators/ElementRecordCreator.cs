using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace StyledBlazor.CodeGenerators
{
    [Generator]
    public class ElementRecordCreator : ISourceGenerator
    {
        private const string NamespaceAndClassOpening = @"
using System;
namespace StyledBlazor
{
    public partial record HelloWorld
    {        
";

        private const string RecordFormat = @"
        public record {0} : Styled
        {{
            public {0}() : base(""{1}"")
            {{
            }}
        
            public {0}(string cssClass = "",
                Func<Dictionary<string, object>, IEnumerable<string>> cssClassAction = null) : base(""{1}"", cssClass,
                cssClassAction)
            {{
            }}
        }}
";

        private const string NamespaceAndClassClosing = @"
    }
}";

        public void Execute(GeneratorExecutionContext context)
        {
            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(NamespaceAndClassOpening);

            sourceBuilder.Append(NamespaceAndClassClosing);

            foreach (var element in DomElements.Elements)
            {
                var upper = FirstCharToUpper(element);
                sourceBuilder.AppendFormat(RecordFormat, upper, element);
            }

            sourceBuilder.Append(NamespaceAndClassClosing);


            // inject the created source into the users compilation
            context.AddSource("ElementRecords", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }

        public static string FirstCharToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
