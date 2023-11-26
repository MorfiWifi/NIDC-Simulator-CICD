using AbrBlazorTools;
using Models;
using Models.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Panel16.ApiUtils
{
    public class ConfigsApi : IConfigApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public ConfigsApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<ConfigModel> AddConfig(ConfigModel config)
        {
            var res =await _http.HttpPost($"/api/config/AddNewConfig", config);
            return JsonConvert.DeserializeObject<ConfigModel>(await res.Content.ReadAsStringAsync());    
        }

        public async Task<HttpResponseMessage> DeleteConfig(Guid id)
        {
            var res = await _http.HttpDelete($"/api/config/DeleteConfig?configId={id}");
            return res;
        }

        public async Task<List<ConfigModel>> GetConfigs()
        {
            var res = await _http.HttpGet($"/api/config/GetUserConfigs");
            return JsonConvert.DeserializeObject<List<ConfigModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<ConfigDetails> GetConfigDetails(string id)
        {
            var res = await _http.HttpGet($"/api/config/GetConfigDetailsAndUnits?configId={id}");
            var cd= JsonConvert.DeserializeObject<ConfigDetails>(await res.Content.ReadAsStringAsync());
            if (cd.Holes == null)
                cd.Holes = new();
            return cd;
        }

        public async Task<List<AccountConfigModel>> GetAccountConfigs(Guid configId)
        {
            var res = await _http.HttpGet($"/api/config/GetConfigAccounts?configId={configId}");
            return JsonConvert.DeserializeObject<List<AccountConfigModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<List<AccountConfigModel>> SaveAccountConfigs(List<AccountConfigModel> accountConfigModels)
        {
            var res =await _http.HttpPost($"/api/config/SaveAccountConfigs", accountConfigModels);
            return JsonConvert.DeserializeObject<List<AccountConfigModel>>(await res.Content.ReadAsStringAsync());    
        }


        public async Task<HttpResponseMessage> SaveConfigDetails(string id,ConfigDetails model)
        {
            var res = await _http.HttpPost($"/api/config/SaveConfigDetails?configId={id}",model);
            return res;
        }

        public async Task<HttpResponseMessage> CopyConfig(Guid id)
        {
            var res = await _http.HttpGet($"/api/config/CopyConfig?configId={id}");
            return res;
        }
    }
    public interface IConfigApi
    {
        Task<List<ConfigModel>> GetConfigs();
        Task<ConfigModel> AddConfig(ConfigModel config);
        Task<HttpResponseMessage> DeleteConfig(Guid id);
        Task<HttpResponseMessage> CopyConfig(Guid id);

        Task<HttpResponseMessage> SaveConfigDetails(string id, ConfigDetails model);
        Task<ConfigDetails> GetConfigDetails(string id);
        Task<List<AccountConfigModel>> GetAccountConfigs (Guid configId);
        Task<List<AccountConfigModel>> SaveAccountConfigs (List<AccountConfigModel> accountConfigModels);
       
    }
}
