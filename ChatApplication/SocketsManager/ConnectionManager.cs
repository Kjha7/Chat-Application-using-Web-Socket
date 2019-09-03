/*
 * class that keeps all active sockets in a thread-safe collection
 * and assigns each a unique ID, while also maintaining the collection (getting, adding and removing sockets).
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.SocketsManager
{
    public class ConnectionManager
    {
         private ConcurrentDictionary<string, WebSocket> connections = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return connections.FirstOrDefault(x => x.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnections()
        {
            return connections;
        }

        public string GetId(WebSocket webSocket)
        {
            return connections.FirstOrDefault(x => x.Value == webSocket).Key;
        }

        public async Task RemoveSocketAsync(string id)
        {
            connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket closed", CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            connections.TryAdd(GetConnectionId(), socket);
        }

        private string GetConnectionId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
