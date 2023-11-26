using Microsoft.AspNetCore.SignalR;
using Models.Config;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Services.Configs;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HafariApi.Hubs
{
    public class SimulationHub : Hub
    {
        private readonly ISimulationService simulationService;
        private readonly JsonSerializerSettings settings;

        public SimulationHub(ISimulationService simulationService)
        {
            this.simulationService = simulationService;
            settings = new JsonSerializerSettings();
            var floatConverter = new LawAbidingFloatConverter();
            settings.Converters.Add(floatConverter);
        }

        [HubMethodName("UpdateModel")]
        public Task UpdateFields(SimulationModel model)
        {
            // save to sql ever 1 minute:
            if (model.ModifyDate.HasValue && DateTime.Now.Subtract(model.ModifyDate.Value).TotalSeconds > 60)
            {
                model.ModifyDate = DateTime.Now;
                var sim = simulationService.GetById(model.Id.Value);
                sim.SimulationFieldsJson = JsonConvert.SerializeObject(model.SimulationFeilds,settings);
                simulationService.Update(sim);
            }
            //Redis:

            // send current model to other users
            var current = Context.ConnectionId;
            //save to redis:
            var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
            options.Password = Consts.RedisPassword;
            using (var redis = ConnectionMultiplexer.Connect(options))
            {                
                options.AsyncTimeout = 1000;
                IDatabase db = redis.GetDatabase();
                db.StringSet($"{model.Id}.in", JsonConvert.SerializeObject(model.SimulationFeilds.InputValues, settings));
                db.StringSet($"{model.Id}.other", JsonConvert.SerializeObject(model.SimulationFeilds.TempValues,settings));

            }
            return Clients.AllExcept(current).SendAsync("update-model", model);
        }
        
        [HubMethodName("UpdateOutputs")]
        public Task UpdateOutputs(SimulationModel model)
        {
            var current = Context.ConnectionId;           
            return Clients.AllExcept(current).SendAsync("Update-Outputs", model);
        }
        
        [HubMethodName("ToggleTong")]
        public Task ToggleTong(SimulationModel model)
        {
            var current = Context.ConnectionId;           
            return Clients.AllExcept(current).SendAsync("Toggle-Tong", model);
        }
        
        [HubMethodName("ToggleSilipsStand")]
        public Task ToggleSilipsStand(SimulationModel model)
        {
            var current = Context.ConnectionId;           
            return Clients.AllExcept(current).SendAsync("Toggle-SilipsStand", model);
        }


        [HubMethodName("AnimationFinished")]
        public Task AnimationFinished(string name)
        {
            var current = Context.ConnectionId;           
            return Clients.AllExcept(current).SendAsync("Animation-Finished", name);
        }
        
        
        [HubMethodName("SaveUiState")]
        public async Task PersistState (string id ,JObject state)
        {
            var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
            options.Password = Consts.RedisPassword;
            await using var redis = await ConnectionMultiplexer.ConnectAsync(options);
            options.AsyncTimeout = 1000;
            var db = redis.GetDatabase();
            db.StringSet($"{id}.state", state.ToString());
        }
        
        [HubMethodName("LoadUiState")]
        public async Task<JObject> LoadUiState(string id)
        {
            JObject parsedJson = null;
            
            try
            {
                var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
                options.Password = Consts.RedisPassword;
                await using var redis = await ConnectionMultiplexer.ConnectAsync(options);
                options.AsyncTimeout = 1000;
                var db = redis.GetDatabase();
                var strObj = db.StringGet($"{id}.state");
                
                parsedJson = JObject.Parse(strObj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //todo use Logger instead
            }

            return parsedJson;
        }

        public override Task OnConnectedAsync()
        {            
            return base.OnConnectedAsync();
        }
    }


}
