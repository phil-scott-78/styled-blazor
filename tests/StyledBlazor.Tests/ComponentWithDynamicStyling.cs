using Alerts;
using Bunit;
using Xunit;

namespace StyledBlazor.Tests
{
    public class ComponentWithDynamicStyling
    {
        [Theory]
        [InlineData("primary", AlertType.Primary)]
        [InlineData("secondary", AlertType.Secondary)]
        public void Can_render_dynamic_alert(string alertTypeString, AlertType alertType)
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<Alert>(
                ComponentParameterFactory.ChildContent("alert text"),
                ("type", alertType)
            );

            cut.MarkupMatches(
                $"<div class=\"alert alert-{alertTypeString}\" role=\"alert\">alert text</div>");
        }
    }
}
