using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AbrBlazorTools;
using Models.Identity;
using Newtonsoft.Json;

namespace Panel16.ApiUtils
{
    public class UserApi:IUserApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public UserApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<List<AccountModel>> SearchUsers(string text)
        {
            var res = await _http.HttpGet($"/api/user/SearchUsers?textToSearch={text}&organizaiotnUrl={Statics.OrganizationUrl}",false);
            return JsonConvert.DeserializeObject<List<AccountModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> Invite(Guid userId)
        {
            var res = await _http.HttpGet($"/api/user/InviteUserToOurCompany?userId={userId}");
            return res;
        }
        public async Task<List<AccountModel>> GetMyInvitedUsers()
        {
            var res = await _http.HttpGet($"/api/user/GetMyInvitedUsers");
            return JsonConvert.DeserializeObject<List<AccountModel>>(await res.Content.ReadAsStringAsync());
        }


        public async Task<List<AccountModel>> GetAll(string organizationId)
        {
            var res = await _http.HttpGet($"/api/user/GetAll?organizationId={organizationId}");
            return JsonConvert.DeserializeObject<List<AccountModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<List<AccountModel>> GetAllByOrganizationUrl(string organizationUrl)
        {
            var res = await _http.HttpGet($"/api/user/GetAllByOrganiztionUrl?organizationUrl={organizationUrl}");
            return JsonConvert.DeserializeObject<List<AccountModel>>(await res.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> ConvertToRole(Guid userId,string role)
        {
            var res = await _http.HttpGet($"/api/user/ConvertToRole?userId={userId}&role={role}");
            return res;
        }

        public async Task<HttpResponseMessage> RemoveRole(Guid userId,string role)
        {
            var res = await _http.HttpGet($"/api/user/RemoveRole?userId={userId}&role={role}");
            return res;
        }

        public async Task<HttpResponseMessage> SetUserActivation(Guid userId, bool isActive)
        {
            var res = await _http.HttpPost($"/api/user/SetUserActivation?userId={userId}" , isActive);
            return res;
        }

        public async Task<HttpResponseMessage> SetUserConfigs(Guid userId, int configsCount)
        {
            var res = await _http.HttpPost($"/api/user/SetUserConfigs?userId={userId}" , configsCount);
            return res;
        }

        public async Task<HttpResponseMessage> SetUserSimulationLength(Guid userId, int simulationLength)
        {
            var res = await _http.HttpPost($"/api/user/SetUserSimulationLength?userId={userId}" , simulationLength);
            return res;
        }
    }

    public interface IUserApi
    {
        Task<List<AccountModel>> SearchUsers(string text);
        Task<HttpResponseMessage> Invite(Guid userId);
        public Task<List<AccountModel>> GetMyInvitedUsers();
        public Task<List<AccountModel>> GetAll(string organizationId);
        public Task<List<AccountModel>> GetAllByOrganizationUrl(string organizationUrl);
        public Task<HttpResponseMessage> ConvertToRole(Guid userId,string role);
        public Task<HttpResponseMessage> RemoveRole(Guid userId,string role);
        public Task<HttpResponseMessage> SetUserActivation(Guid userId , bool isActive);
        public Task<HttpResponseMessage> SetUserConfigs(Guid userId , int configsCount);
        public Task<HttpResponseMessage> SetUserSimulationLength(Guid userId , int simulationLength);
    }
}
