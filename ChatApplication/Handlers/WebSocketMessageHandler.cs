using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.SocketsManager;

namespace ChatApplication.Handlers
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = Connection.GetId(socket);
            await SendMessageToAll(socketId +"Just joined the chat room ");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = Connection.GetId(socket);
            var message = socketId + "said: "+ Encoding.UTF8.GetString(buffer, 0, result.Count);
            await SendMessageToAll(message);
        }
    }
}
