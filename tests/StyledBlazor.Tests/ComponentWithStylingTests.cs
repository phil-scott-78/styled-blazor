using System.Collections.Generic;
using Bunit;
using Xunit;

namespace StyledBlazor.Tests
{
    internal record CustomStyledDiv() : Styled.Div("mb-4")
    {
        protected override IEnumerable<StyledAttribute> Attributes()
        {
            yield return new("style", "border: 1px solid #ccc;");
        }
    }

    public class ComponentWithStylingTests
    {
        [Fact]
        public void Additional_styling_works_as_expected()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<CustomStyledDiv>(
                ComponentParameterFactory.ChildContent("alert text"),
                ("class", "mt-4"),
                ("style", "padding-top:4px;")
            );

            cut.MarkupMatches(
                "<div class=\"mt-4 mb-4\" style=\"border: 1px solid #ccc;padding-top:4px;\">alert text</div>");
        }
    }
}
