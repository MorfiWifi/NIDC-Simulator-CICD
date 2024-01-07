using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AbrBlazorTools;
using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using MudBlazor.Services;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using AbrTools;
using Microsoft.AspNetCore.Localization;
using Panel16.AbrTools;
using Panel16.ApiUtils;
using Microsoft.AspNetCore.StaticFiles;
using Models.Config;

namespace Panel16
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // var baseUrl = "https://localhost:44331/";
            // var baseUrl = "https://localhost:5001/";
            var baseUrl = "https://api.wsim-app.ir/";

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
            builder.Services.AddSweetAlert2();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
            builder.Services.AddScoped<ILoadingSpinner, LoadingSpinner>();
            builder.Services.AddScoped<IHttpClientWithLoginToken, HttpClientWithLoginToken>();
            builder.Services.AddScoped<IBrowserTools, BrowserTools>();
            builder.Services.AddScoped<IThreeJsClient, ThreeJsCilent>();
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddMudServices();
            builder.Services.AddLocalization();            
            #region Api Utils
            builder.Services.AddScoped<IUserApi, UserApi>();
            builder.Services.AddScoped<IOrganizationApi, OrganizationApi>();
            builder.Services.AddScoped<IFolderApi, FolderApi>();
            builder.Services.AddScoped<IConfigApi, ConfigsApi>();
            builder.Services.AddScoped<IUnitApi, UnitApi>();
            builder.Services.AddScoped<ISimulationApi, SimulationApi>();
            builder.Services.AddSingleton<IChannel<SimulationModel>, SimulationChanel>();
            

            #endregion

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".dae", "text/xml");
            builder.Services.Configure<StaticFileOptions>(options =>
            {
                options.ContentTypeProvider = provider;
            });

            var host = builder.Build();

            await host.SetDefaultCulture();
            await host.RunAsync();
        }
    }
}
