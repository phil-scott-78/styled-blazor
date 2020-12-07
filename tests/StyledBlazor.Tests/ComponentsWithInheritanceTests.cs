using Bunit;
using Xunit;

namespace StyledBlazor.Tests
{
    internal record BaseDiv(string ChildClass) : Styled.Div(
        ("class", "mb-4"),
        ("class", ChildClass),
        ("style", "border: 1px solid #ccc;")
    );

    internal record InheritedDiv() : BaseDiv("mt-4");

    public class ComponentsWithInheritanceTests
    {
        [Fact]
        public void Inheritance_works_right()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<InheritedDiv>(
                ComponentParameterFactory.ChildContent("my content")
            );

            cut.MarkupMatches(
                @"<div class=""mb-4 mt-4"" style=""border:1px solid #ccc"">my content</div>");
        }
    }
}
