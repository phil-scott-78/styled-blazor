using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledBlazor
{
    public static class AttributeHelpers
    {
        public static string HasAttribute(this Dictionary<string, string> attributes, string name, string trueValue,
            string falseValue = null)
        {
            return attributes.ContainsKey(name) ? trueValue : falseValue;
        }

        public static string HasAttributeValue(this Dictionary<string, string> attributes, string name, string value,
            string trueValue, string falseValue = null)
        {
            if (attributes.ContainsKey(name))
            {
                if (string.Equals(attributes[name], value, StringComparison.OrdinalIgnoreCase))
                {
                    return trueValue;
                }
                else
                {
                    return falseValue;
                }
            }
            else
            {
                return falseValue;
            }
        }
    }

    public partial record Styled : ComponentBaseRecord
    {
        private readonly string _control;
        private readonly Func<Dictionary<string, string>, IEnumerable<string>> _cssClassAction;

        public Styled(string control, Func<Dictionary<string, string>, IEnumerable<string>> cssClassAction = null)
        {
            _control = control;
            _cssClassAction = cssClassAction;
        }

        public Styled(string control, string cssClass)
        {
            _control = control;
            _cssClassAction = _ => new List<string> {cssClass};
        }

        public Styled(string control)
        {
            _control = control;
            _cssClassAction = null;
        }

        public virtual string CssProperties()
        {
            return string.Empty;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var attributesForCssComparision =
                AdditionalAttributes?.ToDictionary(entry => entry.Key, entry => entry.Value.ToString())
                ?? new Dictionary<string, string>();
            var classArray = _cssClassAction != null
                ? _cssClassAction(attributesForCssComparision)
                : Enumerable.Empty<string>();
            var cssClass = string.Join(" ", classArray.Where(i => string.IsNullOrWhiteSpace(i) == false));

            var attributes = AdditionalAttributes?.ToDictionary(entry => entry.Key, entry => entry.Value) ??
                             new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                if (attributes.ContainsKey("class"))
                {
                    // add logic to be smarter about duplicate css classes
                    attributes["class"] = attributes["class"] += $" {cssClass}";
                }
                else
                {
                    attributes.Add("class", cssClass);
                }
            }

            if (!string.IsNullOrWhiteSpace(CssProperties()))
            {
                if (attributes.ContainsKey("style"))
                {
                    // add logic to be smarter about duplicate css classes
                    attributes["style"] = attributes["style"] += $";{CssProperties()}";
                }
                else
                {
                    attributes.Add("style", CssProperties());
                }
            }

            builder.OpenElement(0, _control);
            builder.AddMultipleAttributes(1, attributes.Select(i => new KeyValuePair<string, object>(i.Key, i.Value)));
            builder.AddContent(2, this.ChildContent);
            builder.CloseElement();
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }
    }
}
