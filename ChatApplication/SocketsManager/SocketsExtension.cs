using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.SocketsManager
{
    public static class SocketsExtension
    {
        /*
         * Besides from adding the WebSocketConnectionManager service,
         * it also searches the executing assembly for types that inherit WebSocketHandler
         * and it registers them as singleton (so that every request gets the same instance of the message handler) using reflection.
         * */
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                    services.AddSingleton(type);
            }

            return services;
        }

        /*
         * It receives a path and it maps that path using with the WebSocketManagerMiddleware
         * which is passed the specific implementation of WebSocketHandler you provided as argument for the MapWebSocketManager extension 
         * method.
         * */
        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path, SocketHandler socket)
        {
            return app.Map(path, (x) => x.UseMiddleware<SocketMiddleware>(socket));
        }
    }
}
