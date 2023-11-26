using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AbrBlazorTools;
using Models;
using Models.BaseData;
using Models.Identity;
using Newtonsoft.Json;

namespace Panel16.ApiUtils
{
    public class FolderApi:IFolderApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public FolderApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var res = await _http.HttpGet($"/api/folder/Remove?folderId={id}");
            return res;
        }

        public async Task<List<FolderModel>> GetAll()
        {
            var res=await _http.HttpGet("/api/Folder/GetAll");
            return JsonConvert.DeserializeObject<List<FolderModel>>(await res.Content.ReadAsStringAsync());
        }

        

        public async Task<HttpResponseMessage> Save(FolderModel org)
        {
            var res = await _http.HttpPost($"/api/Folder/Save",org);
            return res;
        }
    }

    public interface IFolderApi
    {                        
        public Task<List<FolderModel>> GetAll();        
        public Task<HttpResponseMessage> Delete(Guid id);
        public Task<HttpResponseMessage> Save(FolderModel org);

    }
}
