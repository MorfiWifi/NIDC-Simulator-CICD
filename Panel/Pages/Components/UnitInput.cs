using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Models.Config;
using MudBlazor;
using MudBlazor.Extensions;

namespace Panel16.Pages.Components
{
    public abstract class UnitInput<T> : ComponentBase //InputBase<T>
    {
        [Parameter] public T? Value { get; set; }

        [Parameter] public bool hideSystem { get; set; } = false;

        [Parameter] public Enums.UnitCategory unitCat { get; set; }

        [Parameter] public List<UnitModel> units { get; set; }

        [Parameter] public bool noGutters { get; set; }

        [Parameter] public bool ReadOnly { set; get; } = false;
        private UnitModel _unit;

        protected override void OnInitialized()
        {
            _unit = units?.FirstOrDefault(x => x.Category == unitCat.ToString());
            base.OnInitialized();
        }

        public string System => _unit?.System ?? "";
        public double Coefficient => _unit?.Coefficient ?? 0;


        public abstract T GetViewValue();

        public abstract T GetSiValue(object value);


        protected abstract bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out T result,
            [NotNullWhen(false)] out string? validationErrorMessage);

        [Parameter] public string @class { get; set; }
        [Parameter] public bool W100 { get; set; } = true;



        /// <summary>
        /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
        /// properties. Derived components should typically use this value for the primary HTML element's
        /// 'class' attribute.
        /// </summary>
        protected string CssClass
        {
            get => string.IsNullOrEmpty(@class) ? (W100 ? " w-100" : "") : @class;
            set => @class = value + (W100 ? " w-100" : "");
        }


        /// <summary>
        /// on input change awake parent for si value (real stored value)
        /// </summary>
        public async Task HandleInput(ChangeEventArgs args)
        {
            try
            {
                var value = GetSiValue(args.Value);
                await ValueChanged.InvokeAsync(value);
            }
            catch (Exception e)
            {
            }
        }

        [Parameter] public EventCallback<T> ValueChanged { get; set; }

        protected T? CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<T>.Default.Equals(value, Value);
                if (hasChanged)
                {
                    Value = value;
                    _ = ValueChanged.InvokeAsync(Value);
                    // EditContext.NotifyFieldChanged(FieldIdentifier);
                }
            }
        }
    }
}