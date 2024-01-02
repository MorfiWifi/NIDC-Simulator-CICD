using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SimulationInputValues;
using SimulationOutPutValues;
using StackExchange.Redis;
using System.Threading.Tasks;
using System;
using HafariApi.Hubs;
using Infrastructure;
using Models.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Services.Configs;

namespace HafariApi.Controllers
{
    public class BackgroundServiceController : Controller
    {
        private readonly IHubContext<SimulationHub> _hubContext;
        private readonly ISimulationService _simulationService;
        public BackgroundServiceController(IHubContext<SimulationHub> hubContext, ISimulationService simulationService)
        {
            _hubContext = hubContext;
            _simulationService = simulationService;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDataFromRedis()
        {
            var options = ConfigurationOptions.Parse("aberama.iran.liara.ir:32815"); // host1:port1, host2:port2, ...
            options.Password = "4YKFnubfFFjfh4yTK7b0Rg9X";
            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();
                var str = db.StringGet("Simulations");
                if (!str.HasValue)
                {
                    await Task.Delay(100);
                    return Ok();
                }
                var sims = JsonConvert.DeserializeObject<dynamic>(str);
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
                            await Task.Delay(100);
                            continue;
                        }
                        var simulationFilds = new SimulationFeilds
                        {
                            InputValues = JsonConvert.DeserializeObject<InputRoot>("" + simIn),
                            OutputVals = JsonConvert.DeserializeObject<OutputRoot>("" + simOut),
                            TempValues = JsonConvert.DeserializeObject<TempValues>("" + simOther)
                        };
                        await _hubContext.Clients.All.SendAsync("update-model", simulationFilds);
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                }
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRedis()
        {
            var options = ConfigurationOptions.Parse("78.109.201.86:6379"); // host1:port1, host2:port2, ...
            options.Password = "1qazxsw2$$";
            options.AsyncTimeout = 1000;
            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();

                var sims = _simulationService.GetQueryable().Where(x => x.IsRunning);
                if (!sims.Any())
                    return Ok();
                var json = JsonConvert.SerializeObject(sims.Select(x => new { x.Id, Runing = true }));
                db.StringSet("Simulations", json);
                foreach (var sim in sims)
                {
                    var simstr = JsonConvert.DeserializeObject<SimulationFeilds>(sim.SimulationFieldsJson);

                    db.StringSet($"{sim.Id}.in", JsonConvert.SerializeObject(simstr.InputValues));
                    db.StringSet($"{sim.Id}.out", JsonConvert.SerializeObject(simstr.OutputVals));
                    db.StringSet($"{sim.Id}.other", JsonConvert.SerializeObject(simstr.TempValues));

                }
            }
            return Ok();
        }
    }
}
