﻿using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Panel16.Shared.ResourceFiles;
using Panel16;

namespace AbrBlazorTools
{
    public class BrowserTools : IBrowserTools
    {
        private readonly IJSRuntime _js;
        private readonly IStringLocalizer<Panel16.Shared.ResourceFiles.Resource> _localizer;
        private readonly SweetAlertService _sweetAlert;

        public BrowserTools(IJSRuntime js, IStringLocalizer<Resource> localizer, SweetAlertService sweetAlert)
        {
            _js = js;
            _localizer = localizer;
            _sweetAlert = sweetAlert;
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("methods.getDimensions");
        }

        public async Task CopyText(string text, string title = "", bool toast = false)
        {
            await _js.InvokeVoidAsync("navigator.clipboard.writeText", text);
            if (toast)
                await _sweetAlert.ShowToast($"{title} {_localizer[LocalizationKeys.Text_CopyToast]}");
        }

        public async Task PrepareResponsiveTable()
        {
            await _js.InvokeVoidAsync("methods.PrepareResponsiveTable");
        }


        public async Task Prepare3d()
        {
            await _js.InvokeVoidAsync("window.Initi3d");
        }

        public async Task Prepare3dTest()
        {
            await _js.InvokeVoidAsync("window.InitThree");
        }

        public async Task SetMainChart(string identity, double[] array, string[] labels, int chartId)
        {
            await _js.InvokeVoidAsync("window.Charts.main", identity, array, labels, chartId);
        }

        public async Task SetVerticalGaugeValue(string id, double value, double ub, double lb)
        {
            await _js.InvokeVoidAsync("window.setVerticalGaugeValue", id, value, ub, lb);
        }

        public async Task InitSliderHandler(string id)
        {
            await _js.InvokeVoidAsync("window.initSliderHandler", id);
        }

        public async Task SetSlider(double value)
        {
            await _js.InvokeVoidAsync("window.setSlider", value);
        }

        public async Task InitBop()
        {
            await _js.InvokeVoidAsync("window.iniBop");
        }

        public async Task UpdateChokeChart(string id, double[][] arr, string[] labels, string contextType = "2d")
        {
            await _js.InvokeVoidAsync("window.UpdateChokeChart", id, arr, labels, contextType);
        }

        public async Task InitChockJs()
        {
            await _js.InvokeVoidAsync("window.InitilaizeChockJs");
        }

        public async Task InitPipeEvents()
        {
            await _js.InvokeVoidAsync("window.initPipeEvents");
        }

        public async Task KillChart(string chartName)
        {
            await _js.InvokeVoidAsync("window.killChart", chartName);
        }
    }

    public class BrowserDimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public interface IBrowserTools
    {
        Task<BrowserDimension> GetDimensions();
        Task CopyText(string text, string title = "", bool toast = false);
        Task PrepareResponsiveTable();
        Task Prepare3d();
        Task SetMainChart(string identity, double[] array, string[] labels, int chartId);
        Task SetVerticalGaugeValue(string id, double value, double ub, double lb);
        Task InitSliderHandler(string id);
        Task SetSlider(double value);
        Task InitChockJs();
        Task InitBop();
        Task UpdateChokeChart(string id, double[][] arr, string[] labels, string contextType = "2d");

        Task Prepare3dTest();
        Task InitPipeEvents();
        Task KillChart(string chartName);
    }
}