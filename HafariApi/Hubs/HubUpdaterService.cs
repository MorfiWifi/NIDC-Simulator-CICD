using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using HafariApi.Hubs;
using StackExchange.Redis;
using Newtonsoft.Json;
using Infrastructure;
using Models.Config;
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
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await MonitorRedisSimulations();
                }
                catch (Exception)
                {
                    await Task.Delay(2000);
                }
            }
        }

        private async Task MonitorRedisSimulations()
        {
            var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
            options.Password = Consts.RedisPassword;
            await using (var redis = await ConnectionMultiplexer.ConnectAsync(options))
            {
                IDatabase db = redis.GetDatabase();
                var str = db.StringGet("Simulations");
                if (!str.HasValue)
                {
                    await Task.Delay(2200);
                    return;
                }

                var sims = JsonConvert.DeserializeObject<dynamic>(str, settings);
                foreach (var item in sims)
                {
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

                        var simulationFields = new SimulationFeilds
                        {
                            OutputVals = JsonConvert.DeserializeObject<OutputRoot>("" + simOut, settings),
                        };
                        await _hubContext.Clients.All.SendAsync("Update-Outputs",
                            new SimulationModel {SimulationFeilds = simulationFields, Id = item.Id});
                    }
                    catch (Exception ex)
                    {
                        ;Console.WriteLine("exception While Monitoring Redis: {0} , Mess: {1}" , ex  , ex.Message);
                    }
                }
            }

            await Task.Delay(Consts.SimulationStep);
        }
    }
}