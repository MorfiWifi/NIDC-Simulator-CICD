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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HafariApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ISimulationService simulationService;
        private readonly JsonSerializerSettings settings;

        public ChatHub()
        {
            // this.simulationService = simulationService;
            // settings = new JsonSerializerSettings();
            // var floatConverter = new LawAbidingFloatConverter();
            // settings.Converters.Add(floatConverter);
        }
        
        [HubMethodName("send")]
        public async Task SendMessage( string simulationId ,string message )
        {
            var myConId = Context.ConnectionId;
            Console.WriteLine($"User Identity Name With ConId: {myConId} -> {Context?.User?.Identity?.Name}");
            await Clients.GroupExcept(simulationId, myConId).SendAsync( "new-message" , message);
            
            Console.WriteLine($"{myConId} => {message} to {simulationId}");
        }
        
        [HubMethodName("leave")]
        public async Task SendMessage( string simulationId)
        {
            var myConId = Context.ConnectionId;
            await Groups.RemoveFromGroupAsync(myConId, simulationId);
            
            Console.WriteLine($"{myConId} => leaving {simulationId}");
        }
        
        [HubMethodName("join")]
        public async Task JoinSimulationGroup ( string simulationId)
        {
            var myConId = Context.ConnectionId;
            await Groups.AddToGroupAsync(myConId, simulationId);
            
            Console.WriteLine($"{myConId} => joining {simulationId}");
        }
        

        public override Task OnConnectedAsync()
        {            
            return base.OnConnectedAsync();
        }
    }


}
