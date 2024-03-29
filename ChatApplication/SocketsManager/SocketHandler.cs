﻿/*
 * class that handles connection and disconnection events and
 * manages sending and receiving messages from the socket. 
 */

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication.SocketsManager
{
    /*
     *  you need to inherit it and provide actual implementation for the ReceiveAsync method,
     *  as well as you can override the methods marked as virtual.
     */
    public abstract class SocketHandler
    {
        public ConnectionManager Connection { get; set; }
        public SocketHandler(ConnectionManager connection)
        {
            Connection = connection;
        }
        protected SocketHandler() { }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => { Connection.AddSocket(socket); });
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await Connection.RemoveSocketAsync(Connection.GetId(socket));
        }

        public async Task SendMessage(WebSocket socket,string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message),0, message.Length), WebSocketMessageType.Text,true, CancellationToken.None);
        }

        public async Task SendMessage(string id, string message)
        {
            await SendMessage(Connection.GetSocketById(id), message);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach(var con in Connection.GetAllConnections())
                await SendMessage(con.Value, message);
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
