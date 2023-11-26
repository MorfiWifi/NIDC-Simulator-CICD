using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Infrastructure;

namespace AbrBlazorTools
{
    public class HttpClientWithLoginToken : IHttpClientWithLoginToken
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILocalStorageService _localStorage;
        private readonly CurrieTechnologies.Razor.SweetAlert2.SweetAlertService _swal;
        private readonly AbrBlazorTools.ILoadingSpinner _loadingSpinner;
        private readonly NavigationManager _navigationManager;
        private readonly IBrowserTools _browserTools;
        public HttpClientWithLoginToken(ILocalStorageService localStorage, CurrieTechnologies.Razor.SweetAlert2.SweetAlertService swal
            , AbrBlazorTools.ILoadingSpinner loadingSpinner, NavigationManager navigationManager, IBrowserTools browserTools)
        {
            _localStorage = localStorage;
            _swal = swal;
            _loadingSpinner = loadingSpinner;
            _navigationManager = navigationManager;
            _browserTools = browserTools;
            _httpClient.BaseAddress = new Uri(Consts.BaseAddress);
            System.Net.ServicePointManager.Expect100Continue = false;
        }
        public async Task<HttpClient> GetInstance()
        {
            var hasToken = await _localStorage.ContainKeyAsync(Statics.LoginTokenKey);
            if (hasToken)
            {
                var token = await _localStorage.GetItemAsStringAsync(Statics.LoginTokenKey);
                if (token.StartsWith('"'))
                    token = token.Substring(1);
                if (token.EndsWith('"'))
                    token = token.Substring(0, token.Length - 1);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.ConnectionClose = false;
                System.Net.ServicePointManager.Expect100Continue = false;
                _httpClient.DefaultRequestHeaders.ExpectContinue = false;
                System.Net.ServicePointManager.SetTcpKeepAlive(true, 1000, 1000);
                _httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            }
            return _httpClient;
        }

        public async Task<HttpResponseMessage> HttpGet(string url, bool hasLoading = true)
        {
            if (hasLoading)
                await _loadingSpinner.ShowLoading();
            var http = await GetInstance();
            try
            {
                var res = await http.GetAsync(url);
                await _loadingSpinner.HideLoading();
                if (res.IsSuccessStatusCode)
                {
                    await _browserTools.PrepareResponsiveTable();
                    return res;
                }

                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _loadingSpinner.HideLoading();
                    Statics.CurrentLoginInfo = new();

                }
                else if (res.StatusCode == HttpStatusCode.BadRequest)
                {
                    await _swal.FireAsync(title: "Error", message: res.ReasonPhrase, icon: SweetAlertIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await _loadingSpinner.HideLoading();
                await _swal.FireAsync(title: "Error", message: "Error", icon: SweetAlertIcon.Error);
                Statics.CurrentLoginInfo = new();

            }
            await _loadingSpinner.HideLoading();
            return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
        }
        public async Task<HttpResponseMessage> HttpDelete(string url, bool hasLoading = true)
        {
            if (hasLoading)
                await _loadingSpinner.ShowLoading();
            var http = await GetInstance();
            try
            {
                var res = await http.DeleteAsync(url);
                await _loadingSpinner.HideLoading();
                if (res.IsSuccessStatusCode)
                {
                    await _browserTools.PrepareResponsiveTable();
                    return res;
                }
                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _loadingSpinner.HideLoading();
                    Statics.CurrentLoginInfo = new();

                }
                else
                {
                    await _swal.FireAsync(title: "Error", message: res.ReasonPhrase, icon: SweetAlertIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await _loadingSpinner.HideLoading();
                await _swal.FireAsync(title: "Error", message: "Error", icon: SweetAlertIcon.Error);
                Statics.CurrentLoginInfo = new();

            }
            await _loadingSpinner.HideLoading();
            return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
        }

        public async Task<HttpResponseMessage> HttpPost(string url, object model, bool hasLoading = true)
        {
            if (hasLoading)
                await _loadingSpinner.ShowLoading();
            var http = await GetInstance();
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var res = await http.PostAsync(url, content);
                await _loadingSpinner.HideLoading();
                if (res.IsSuccessStatusCode)
                {
                    await _browserTools.PrepareResponsiveTable();
                    return res;
                }
                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _loadingSpinner.HideLoading();
                    await _swal.FireAsync(title: "Error", message: "Unauthorized Access !", icon: SweetAlertIcon.Error);
                    Statics.CurrentLoginInfo = new();

                }
                else
                {
                    await _swal.FireAsync(title: "Error", message: "Unhandeled Error!", icon: SweetAlertIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await _loadingSpinner.HideLoading();
                await _swal.FireAsync(title: "Error", message: "Unhandeled Error!", icon: SweetAlertIcon.Error);

                Statics.CurrentLoginInfo = new();

            }
            await _loadingSpinner.HideLoading();
            return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
        }
    }
    public interface IHttpClientWithLoginToken
    {
        Task<HttpClient> GetInstance();
        Task<HttpResponseMessage> HttpPost(string url, object model, bool hasLoading = true);
        Task<HttpResponseMessage> HttpGet(string url, bool hasLoading = true);
        Task<HttpResponseMessage> HttpDelete(string url, bool hasLoading = true);
    }
}
