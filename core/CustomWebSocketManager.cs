using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuizApp.Core.core
{
   public class CustomWebSocketManager
{
    private readonly RequestDelegate _next;

    public CustomWebSocketManager(RequestDelegate next)
    {
       _next = next;
    }

    public async Task Invoke(HttpContext context, ICustomWebSocketFactory wsFactory,
     ICustomWebSocketMessageHandler wsmHandler)
    {
        if (context.Request.Path == "/ws")
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                string username = context.Request.Query["u"];
                if (!string.IsNullOrEmpty(username))
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    CustomWebSocket userWebSocket = 
                    new CustomWebSocket(webSocket,username+new Random().Next(0,100)); 
                    wsFactory.Add(userWebSocket); 
                    await wsmHandler.HandleNewUserMessage(wsFactory);
                    await Listen(context, userWebSocket, wsFactory, wsmHandler);
                }
            }
            else
            {
                 context.Response.StatusCode = 400;
            }
        }
        await _next(context);
    }

    private async Task Listen(HttpContext context, CustomWebSocket userWebSocket, ICustomWebSocketFactory wsFactory, ICustomWebSocketMessageHandler wsmHandler)
    {
        WebSocket webSocket = userWebSocket.WebSocket;
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        while (!result.CloseStatus.HasValue)
        {
             await wsmHandler.HandleMessage(buffer, wsFactory);
             buffer = new byte[1024 * 4];
             result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        } 
        wsFactory.Remove(userWebSocket.Username);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}

}