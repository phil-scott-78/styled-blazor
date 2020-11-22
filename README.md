# Styled Blazor

Create Blazor Components with a single line of code with the magic of C# 9 records

``` c#
namespace Buttons
{
    public record Primary() : Styled.Button("btn btn-primary");
    public record Secondary() : Styled.Button("btn btn-secondary");
}
```

Each of the above classes are the equivalent of one of these .blazor components

```c#
<button class="btn btn-primary" @attributes="AdditionalAttributes">@ChildContent</button>

@code {
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
```

## Getting Fancy

In addition to specifying the CSS class, you can also specify default attributes and conditionally render the CSS.

```c#
namespace Alerts
{
    public enum AlertType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    public record Alert() : Styled.Div("alert", ("role", "alert"))
    {
        // getting a little fancier, allow different styling based on a parameter
        // of the object and also setting an attribute value
        [Parameter] public AlertType Type { get; set; }

        protected override string CssClasses()
        {
            return Type switch
            {
                AlertType.Primary => "alert-primary",
                AlertType.Secondary => "alert-secondary",
                AlertType.Success => "alert-success",
                AlertType.Danger => "alert-danger",
                AlertType.Warning => "alert-warning",
                AlertType.Info => "alert-info",
                AlertType.Light => "alert-light",
                AlertType.Dark => "alert-dark",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
```

This produces a component that can be used such as 

```c#
<Alerts.Alert Type="AlertType.Info" class="mt-4">
```

## Organization and Namespaces

Razor syntax won't let your use nested classes or partial namespaces. To keep things organized I'd recommend putting your components in a root namespace defining 
them (e.g. Buttons, Alerts, etc) and disabling your IDE's warnings.