using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
  
    public class Test3DController : BaseController
    {
       
        [HttpGet]
        [ProducesResponseType(typeof(List<double>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetNewNmber()
        {
            double[] values =new double[100];
            for(var i=0;i<values.Length;i++)
                values[i] = (new Random()).NextDouble();
            var start = DateTime.Now;            
            return Ok(value:values);
        }

        [HttpGet("/ws")]
        public async Task GetNewNmberWs()
        {
            double[] values = new double[100];
            for (var i = 0; i < values.Length; i++)
                values[i] = (new Random()).NextDouble();
            var start = DateTime.Now;
            var buf= values.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var buffer = new byte[1024 * 4];
                await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, buf.Length),
                        messageType:System.Net.WebSockets.WebSocketMessageType.Binary,
                        endOfMessage:true,
                        CancellationToken.None);
                await webSocket.CloseAsync(closeStatus:System.Net.WebSockets.WebSocketCloseStatus.NormalClosure,"" ,CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
