using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledBlazor
{
    /// <summary>
    /// Represents an attribute of a styled component
    /// </summary>
    public sealed record StyledAttribute(string Name, string Value);

    public partial record Styled : ComponentBaseRecord
    {
        private readonly string _control;
        private readonly string _css;

        public Styled(string control, string css = "")
        {
            _control = control;
            _css = css;
        }

        protected virtual IEnumerable<StyledAttribute> Attributes()
        {
            yield break;
        }

        protected virtual string CssClasses()
        {
            return "";
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // attributes defined at the class definition are overwritten by instance
            // and then we add the CSS. Merge will concat the CSS attribute
            var classAttributes = Attributes().ToDictionary(k => k.Name, v => (object)v.Value);

            var attributes = classAttributes
                .Merge(AdditionalAttributes)
                .Merge(new Dictionary<string, object> {{"class", _css + " " + CssClasses()}});

            builder.OpenElement(0, _control);
            builder.AddMultipleAttributes(1, attributes.Select(i => new KeyValuePair<string, object>(i.Key, i.Value)));
            if (this.ChildContent != null)
            {
                builder.AddContent(2, this.ChildContent);
            }

            builder.CloseElement();
        }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; } =
            new Dictionary<string, object>();
    }

    internal static class DictionaryHelper
    {
        public static IReadOnlyDictionary<string, object> Merge(
            this IReadOnlyDictionary<string, object> targetDict,
            IReadOnlyDictionary<string, object> mergedDict)
        {
            // we'll want all the key and values from the target
            var newDict = targetDict.ToDictionary(i => i.Key, i => i.Value);
            foreach (var (key, value) in mergedDict)
            {
                // try and add from the dictionary of merged values
                // if it already exists we'll need to either overwrite or merge
                // depending on the key
                if (newDict.TryAdd(key, value))
                    continue;

                var previousValue = newDict[key] as string ?? string.Empty;
                newDict[key] = key switch
                {
                    "class" => $"{previousValue.TrimEnd()} {value}",
                    "style" => previousValue + (previousValue.EndsWith(";") ? "" : ";") + value,
                    _ => value
                };
            }

            return newDict;
        }
    }
}
