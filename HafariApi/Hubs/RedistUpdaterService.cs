using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using Services.Configs;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Infrastructure;
using System.Collections.Generic;
using Models.Config;
using Core.Migrations;

namespace HafariApi.Hubs
{
    public class RedisUpdaterService : BackgroundService
    {
        private readonly IHubContext<SimulationHub> _hubContext;
        private readonly JsonSerializerSettings settings;


        public RedisUpdaterService(IHubContext<SimulationHub> hubContext)
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
                    options.AsyncTimeout = 1000;
                    using (var redis = ConnectionMultiplexer.Connect(options))
                    {
                        IDatabase db = redis.GetDatabase();
                        var http = new HttpClient();
                        http.BaseAddress = new Uri(Consts.BaseAddress);
                        var res = await http.GetAsync("/api/Simulation/Get3Last?pass=1qazxsw2$$");
                        if (res.IsSuccessStatusCode)
                        {
                            var sims = JsonConvert.DeserializeObject<List<SimulationModel>>(await res.Content.ReadAsStringAsync(), settings);
                            var json = JsonConvert.SerializeObject(sims.Select(x => new { x.Id, Runing = true }), settings);
                            db.StringSet("Simulations", json);
                            foreach (var sim in sims)
                            {
                                if (db.KeyExists($"{sim.Id}.in"))
                                    continue;
                                sim.SimulationFeilds.InputValues.status = 1;
                                sim.SimulationFeilds = JsonConvert.DeserializeObject<SimulationFeilds>(sim.SimulationFieldsJson, settings);                                
                                db.StringSet($"{sim.Id}.in", JsonConvert.SerializeObject(sim.SimulationFeilds.InputValues, settings));
                                db.StringSet($"{sim.Id}.out", JsonConvert.SerializeObject(sim.SimulationFeilds.OutputVals, settings));
                                db.StringSet($"{sim.Id}.other", JsonConvert.SerializeObject(sim.SimulationFeilds.TempValues, settings));

                            }
                        }
                    }
                    await Task.Delay(10 * 1000);
                }
            }
            catch (Exception)
            {
                await Task.Delay(10 * 1000);

            }
        }
    }
}
