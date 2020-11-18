using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using StyledBlazor;

// ReSharper disable CheckNamespace
// ReSharper disable MissingBlankLines

namespace Buttons
{
    public record Primary() : Styled.Button("btn btn-primary");

    public record Secondary() : Styled.Button("btn btn-secondary");
}

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

    public record Alert() : Styled.Div("alert")
    {
        [Parameter] public AlertType Type { get; set; }

        protected override IEnumerable<StyledAttribute> Attributes()
        {
            yield return new("role", "alert");
        }

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
