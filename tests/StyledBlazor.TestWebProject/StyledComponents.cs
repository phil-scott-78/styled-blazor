using StyledBlazor;

// ReSharper disable MissingBlankLines
// ReSharper disable CheckNamespace
namespace Buttons
{
    public abstract record Default(string Additional) : Styled.Button("btn " + Additional);

    public record Primary() : Default("btn-primary");

    public record Secondary() : Default("btn-primary");
}

namespace Layout
{
    public record Page() : Styled.Div("flex flex-col w-64");

    public record Main() : Styled.Div("main");

    public record Content() : Styled.Div("flex-1 relative z-0 overflow-y-auto focus:outline-non");

    public record Sidebar() : Styled.Div("flex-1 pt-5");
}
