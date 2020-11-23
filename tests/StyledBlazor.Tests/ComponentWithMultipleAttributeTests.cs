using System.Collections.Generic;
using Bunit;
using Xunit;

namespace StyledBlazor.Tests
{
    internal record MultipleAttributeDiv() : Styled.Div(
        ("class", "mb-4"),
        ("class", "mr-4"),
        ("style", "border: 1px solid #ccc"),
        ("style", "font-size: 2rem")
    )
    {
        protected override IEnumerable<StyledAttribute> Attributes()
        {
            yield return ("class", "ml-4");
            yield return ("class", "pb-4");
        }
    }

    public class ComponentsWithMultipleAttributeTests
    {
        [Fact]
        public void Additional_styling_works_as_expected()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MultipleAttributeDiv>(
                ComponentParameterFactory.ChildContent("alert text"),
                ("class", "mt-4"),
                ("style", "padding-top:4px;")
            );

            cut.MarkupMatches(
                @"
<div class=""mt-4 mb-4 mr-4 ml-4 pb-4"" style=""border: 1px solid #ccc;font-size:2rem;padding-top:4px;"">
    alert text
</div>");
        }
    }
}
