using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrBlazorTools
{
    public class LoadingSpinner : ILoadingSpinner
    {
        private readonly IJSRuntime _jSRuntime;
        public LoadingSpinner(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }
        public async Task HideLoading()
        {            
            await _jSRuntime.InvokeVoidAsync("methods.HideLoading", null);
        }

        public async Task ShowLoading()
        {
            await _jSRuntime.InvokeVoidAsync("methods.ShowLoading", null);
        }
    }
    public interface ILoadingSpinner
    {
        Task ShowLoading();
        Task HideLoading();
    }
}
