using Microsoft.AspNetCore.SignalR;
using Models.Config;
using Newtonsoft.Json;
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

public class NumbersHub : Hub
{
    [HubMethodName("SendNumbers")]
    public Task SendNumbers(string text)
    {
        double[] values = new double[100];
        for (var i = 0; i < values.Length; i++)
            values[i] = (new Random()).NextDouble();        
        return Clients.All.SendAsync("ReceiveMessage", JsonConvert.SerializeObject(values));
    }
}
