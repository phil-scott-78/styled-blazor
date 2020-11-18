using Bunit;
using static Bunit.ComponentParameterFactory;
using Xunit;

namespace StyledBlazor.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Primary_button_should_render_as_a_button()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Buttons.Primary>(parameters => parameters.AddChildContent("Click me"));
            cut.MarkupMatches("<button class=\"btn btn-primary\">Click me</button>");
        }

        [Fact]
        public void Extra_attributes_are_included()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Buttons.Primary>(
                ChildContent("Click me"),
                ("role", "button")
            );

            cut.MarkupMatches("<button class=\"btn btn-primary\" role=\"button\">Click me</button>");
        }

        [Fact]
        public void Additional_css_classes_work_as_expected()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Buttons.Primary>(
                ChildContent("Click me"),
                ("class", "mt-4")
            );

            cut.MarkupMatches("<button class=\"btn btn-primary mt-4\">Click me</div>");
        }
    }
}
