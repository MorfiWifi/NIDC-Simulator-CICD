using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Panel16.Pages.Components
{
    public class RangedComponent : ComponentBase
    {
        // bias
        [Parameter] public double From { set; get; } = 0;
        [Parameter] public double To { set; get; } = 6000;

        [CascadingParameter] EditContext EditContext { get; set; } = default!;
        [Parameter]
        public EventCallback<double> ValueChanged { get; set; }
        [Parameter]
        public Expression<Func<double>> ValueExpression { get; set; }
        public double _Value = 0;
        [Parameter]
        public double Value
        {
            get => _Value;
            set
            {
                if (_Value == value) return;
                _Value = value;
                ValueChanged.InvokeAsync(_Value);
                var fieldIdentifier = FieldIdentifier.Create(ValueExpression);
                EditContext.NotifyFieldChanged(fieldIdentifier);
            }
        }

        private double StdScale(double input) => Math.Max(Math.Min((input - From) / (To - From), 1) , 0);
        private double RangeScale(double stdInput) => Math.Max(Math.Min(stdInput * (To - From) + From , To) , From);
    }
}