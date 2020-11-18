# Styled Blazor

Create Blazor Components with a single line of code with the magic of C# 9 records

``` csharp
namespace Buttons
{
    public record Primary() : Styled.Button("btn btn-primary");
    public record Secondary() : Styled.Button("btn btn-secondary");
}
```
