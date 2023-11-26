using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AbrBlazorTools;
using Models.BaseData;
using Models.Identity;
using Newtonsoft.Json;

namespace Panel16.ApiUtils
{
    public class OrganizationApi:IOrganizationApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public OrganizationApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var res = await _http.HttpGet($"/api/Organization/Remove?organizationId={id}");
            return res;
        }

        public async Task<List<OrganizationModel>> GetAll()
        {
            var res=await _http.HttpGet("/api/Organization/GetAll");
            return JsonConvert.DeserializeObject<List<OrganizationModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<OrganizationModel> GetbyId(string id)
        {
            var res = await _http.HttpGet($"/api/Organization/GetById?organizationId={id}");
            return JsonConvert.DeserializeObject<OrganizationModel>(await res.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> Save(OrganizationModel org)
        {
            var res = await _http.HttpPost($"/api/Organization/Save",org);
            return res;
        }
    }

    public interface IOrganizationApi
    {                        
        public Task<List<OrganizationModel>> GetAll();
        public Task<OrganizationModel> GetbyId(string id);
        public Task<HttpResponseMessage> Delete(Guid id);
        public Task<HttpResponseMessage> Save(OrganizationModel org);

    }
}
