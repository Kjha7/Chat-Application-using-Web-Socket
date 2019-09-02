/*
 * With this middleware, you can map different paths of your application with specific implementations of WebSocketHandler,
 * so you would get completely isolated environments 
 * As middleware, it needs to receive a RequestDelegate for the next component in the pipeline,
 * while executing operations on the HttpContext before and after invoking the next component,
 * and it needs an async Task Invoke method. It doesn’t have to inherit or implement anything, just to have the Invoke method.
 */


using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChatApplication.SocketsManager
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private SocketHandler Handler { get; set; }

        public SocketMiddleware(RequestDelegate next, SocketHandler handler)
        {
            _next = next;
            Handler = handler;
        }

        /*
         * If it is a WebSockets request, then it accepts the connection and passes the socket to the OnConnected method from the SocketHandler.
         * while the socket is in the Open state, it awaits for the receival of new data.
         * When it receives the data, it decides wether to pass the context to the ReceiveAsync method of WebSocketHandler
         * or to the OnDisconnected method (if the message type is Close).
         */
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await Handler.OnConnected(socket);
            await Receive(socket, async (result, buffer) =>
            {
                if(result.MessageType == WebSocketMessageType.Text)
                {
                    await Handler.Receive(socket, result, buffer); 
                }

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await Handler.OnDisconnected(socket);
                }
            });
        }

        private async Task Receive(WebSocket webSocket, Action<WebSocketReceiveResult, byte[]> messageHandler)
        {
            var buffer = new byte[1024 * 4];
            while(webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                messageHandler(result, buffer);
            }
        }
    }
}