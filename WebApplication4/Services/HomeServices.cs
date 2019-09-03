using ChatApplication.SocketsManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebApplication4.Configs;
using WebApplication4.Models.Entity;
using WebApplication4.Models.Request;

namespace WebApplication4.Services
{
    public class HomeServices : SocketHandler
    {

        public IMongoCollection<Credentials> credentials;
        public LoginConfig _config;
        public HomeServices(IOptions<LoginConfig> settings)
        {
            _config = settings.Value;
            var client = new MongoClient(_config.Uri);
            var database = client.GetDatabase(_config.Database);
            credentials = database.GetCollection<Credentials>(_config.Collection);
        }

        public bool Login(String username, String password)
        {

            if (username != null && password != null)
            {
                var person = credentials.Find(p => p.Fullname == username && p.Password == password).FirstOrDefault();
                if (person != null)
                {
                    return true;
                }
            }

            return false;
        }

        public override Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new NotImplementedException();
        }
        
    }
}
