using AbrBlazorTools;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Config;
using Models.Config.Simulation;
using Models.Simulations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Panel16.ApiUtils
{
    public class SimulationApi : ISimulationApi
    {
        private readonly IHttpClientWithLoginToken _http;

        public SimulationApi(IHttpClientWithLoginToken http)
        {
            _http = http;
        }

        public async Task<SimulationModel> AddSimulation(SimulationModel sim)
        {
            var res = await _http.HttpPost($"/api/Simulation/Add", sim);
            return JsonConvert.DeserializeObject<SimulationModel>(await res.Content.ReadAsStringAsync());
        }

        public async Task<List<SimulationModel>> GetSimulations()
        {
            var res = await _http.HttpGet($"/api/Simulation/GetAll");
            return JsonConvert.DeserializeObject<List<SimulationModel>>(await res.Content.ReadAsStringAsync());
        }
        public async Task<List<SimulationModel>> GetMySimulations()
        {
            var res = await _http.HttpGet($"/api/Simulation/GetMySimulations");
            return JsonConvert.DeserializeObject<List<SimulationModel>>(await res.Content.ReadAsStringAsync());
        }
        public async Task<SimulationModel> GetById(Guid simId)
        {
            var res = await _http.HttpGet($"/api/Simulation/GetById?simulationId={simId}");
            return JsonConvert.DeserializeObject<SimulationModel>(await res.Content.ReadAsStringAsync());
        }
        public async Task<SimulationModel> GetFromRedis(Guid simId)
        {
            var res = await _http.HttpGet($"/api/Simulation/GetFromRedis?simulationId={simId}");
            if (!res.IsSuccessStatusCode)
                return null;
            var js = await res.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<SimulationModel>(js);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<SimulationUserModel>> GetSimulationUsers(string simulationID)
        {
            var res = await _http.HttpGet($"/api/Simulation/GetSimulationUsers?simulationId={simulationID}");
            return JsonConvert.DeserializeObject<List<SimulationUserModel>>(await res.Content.ReadAsStringAsync());
        }
        public async Task<HttpResponseMessage> AssigUserToSimulation(AssignUserToSimulationModel model)
        {
            var res = await _http.HttpPost($"/api/Simulation/SetUserSimualtionRole",model);
            return  res;
        }

        public async Task<ResponseModel> Delete(Guid simulationId)
        {
            var res = await _http.HttpDelete($"/api/Simulation/Delete?simulationId={simulationId}");
            return JsonConvert.DeserializeObject<ResponseModel>(await res.Content.ReadAsStringAsync());

        }

        public async Task<HttpResponseMessage> Play(Guid simulationId)
        {
            var res = await _http.HttpPost($"/api/Simulation/SetSimulationRuning",new SimpulationSetRunningRequest
            {
                IsRuning = true,
                Id = simulationId
            });
            return res;
        }

        public async Task<HttpResponseMessage> Pause(Guid simulationId)
        {
            var res = await _http.HttpPost($"/api/Simulation/SetSimulationRuning", new SimpulationSetRunningRequest
            {
                IsRuning = false,
                Id = simulationId
            });
            return res;
        }
        public async Task<HttpResponseMessage> Stop(Guid simulationId)
        {
            var res = await _http.HttpPost($"/api/Simulation/SetSimulationRuning", new SimpulationSetRunningRequest
            {
                IsRuning = false,
                Stop=true,
                Id = simulationId
            });
            return res;
        }

        public async Task<HttpResponseMessage> SaveToRedis(SimulationModel sim)
        {
            var res = await _http.HttpPost($"/api/Simulation/SaveToRedis",sim, hasLoading: false);
            return res;
        }
    }
    public interface ISimulationApi
    {
        Task<List<SimulationModel>> GetSimulations();
        Task<List<SimulationModel>> GetMySimulations();

        Task<SimulationModel> AddSimulation(SimulationModel sim);
        Task<SimulationModel> GetById(Guid simId);
        Task<SimulationModel> GetFromRedis(Guid simId);
        Task<HttpResponseMessage> SaveToRedis(SimulationModel sim);


        Task<List<SimulationUserModel>> GetSimulationUsers(string simulationID);
        Task<HttpResponseMessage> AssigUserToSimulation(AssignUserToSimulationModel model);
        Task<ResponseModel> Delete(Guid simulationId);

        Task<HttpResponseMessage> Play(Guid simulationId);
        Task<HttpResponseMessage> Pause(Guid simulationId);
        Task<HttpResponseMessage> Stop(Guid simulationId);







    }
}
