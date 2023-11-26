using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using HafariApi.Hubs;
using StackExchange.Redis;
using Services.Configs;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Infrastructure;
using System.Collections.Generic;
using Models.Config;
using System.Dynamic;
using SimulationOutPutValues;
using SimulationInputValues;


namespace HafariApi
{
    public class HubUpdaterService : BackgroundService
    {
        private readonly IHubContext<SimulationHub> _hubContext;
        private readonly JsonSerializerSettings settings;
        public HubUpdaterService(IHubContext<SimulationHub> hubContext)
        {
            _hubContext = hubContext;
            settings = new JsonSerializerSettings();
            var floatConverter = new LawAbidingFloatConverter();
            settings.Converters.Add(floatConverter);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
                    options.Password = Consts.RedisPassword;
                    using (var redis = ConnectionMultiplexer.Connect(options))
                    {
                        IDatabase db = redis.GetDatabase();
                        var str = db.StringGet("Simulations");
                        if (!str.HasValue)
                        {
                            await Task.Delay(2200);
                            continue;
                        }
                        var sims = JsonConvert.DeserializeObject<dynamic>(str,settings);
                        foreach (var item in sims)
                        {
                            var outkey = $"{item.Id}.out";
                            try
                            {
                                var simOut = db.StringGet($"{item.Id}.out");
                                var simIn = db.StringGet($"{item.Id}.in");
                                var simOther = db.StringGet($"{item.Id}.other");
                                if (!simIn.HasValue || !simOut.HasValue || !simOther.HasValue)
                                {
                                    await Task.Delay(2000);
                                    continue;
                                }
                                
                                //replace Nan !
                                //todo : SignalR receiver Using Default Json Converter
                                //  - unable to convert NaN to double.NaN 
                                simOut = simOut.ToString().Replace("NaN", "0");
                                
                                var simulationFilds = new SimulationFeilds
                                {
                                    OutputVals = JsonConvert.DeserializeObject<OutputRoot>("" + simOut,settings),
                                };
                                await _hubContext.Clients.All.SendAsync("Update-Outputs",new SimulationModel { SimulationFeilds = simulationFilds ,Id=item.Id});
                            }
                            catch (Exception ex)
                            {
                                ;
                            }

                        }
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception)
            {
                await Task.Delay(2000);

            }
        }
       
    }
}
