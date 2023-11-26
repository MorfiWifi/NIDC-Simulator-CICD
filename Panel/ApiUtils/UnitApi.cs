using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AbrBlazorTools;
using Models.Config;
using Models.Identity;
using Newtonsoft.Json;

namespace Panel16.ApiUtils
{
    public class UnitApi:IUnitApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public UnitApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<List<UnitModel>> GetAll()
        {
            var res = await _http.HttpGet($"/api/Unit/GetAll");
            return JsonConvert.DeserializeObject<List<UnitModel>>(await res.Content.ReadAsStringAsync());
        }
        
        public async Task<List<UnitModel>> Get()
        {
            var res = await _http.HttpGet($"/api/Unit/Get");
            return JsonConvert.DeserializeObject<List<UnitModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> Save(List<UnitModel> models)
        {
            var res = await _http.HttpPost($"/api/Unit/Save" , models);
            return res;
        }
        
    }

    public interface IUnitApi
    {
        Task<List<UnitModel>> GetAll ();
        Task<List<UnitModel>> Get ();
        Task<HttpResponseMessage> Save (List<UnitModel> models);
    }
}