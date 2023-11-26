using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Globalization;
using System.Threading.Tasks;

namespace Panel16.AbrTools
{
    public static class WebAssemblyHostExtension
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost host)
        {
            var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
            CultureInfo culture;
            if (result != null)
                culture = new CultureInfo(result);
            else
            {
                await jsInterop.InvokeAsync<string>("blazorCulture.set", "en-US");
                result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
                culture = new CultureInfo(result);
            }
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
