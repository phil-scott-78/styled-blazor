using Bunit;
using Shouldly;
using Xunit;

namespace StyledBlazor.Tests
{
    public class StyledAttributeTest
    {
        [Fact]
        public void Can_implicitly_convert()
        {
            StyledAttribute attribute = ("role", "alert");
            attribute.Name.ShouldBe("role");
            attribute.Value.ShouldBe("alert");
        }

        [Fact]
        public void Can_deconstruct()
        {
            StyledAttribute attribute = ("role", "alert");
            var (name, value) = attribute;
            name.ShouldBe("role");
            value.ShouldBe("alert");
        }
    }
}
