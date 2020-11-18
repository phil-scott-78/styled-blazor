# Styled Blazor

Create Blazor Components with a single line of code with the magic of C# 9 records

``` csharp
namespace Buttons
{
    public record Primary() : Styled.Button("btn btn-primary");
    public record Secondary() : Styled.Button("btn btn-secondary");
}
```

Each of the above classes are the equivalent of one of these .blazor components

```csharp
<button class="btn btn-primary" @attributes="AdditionalAttributes">@ChildContent</button>

@code {
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
```
