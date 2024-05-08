using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace ReactApp1.Server
{
    public class WebSocketHelper
    {
        //private static ConcurrentDictionary<string, WebSocket> _clients = new ConcurrentDictionary<string, WebSocket>();
        private static ConcurrentBag<WebSocket> _clients = new ConcurrentBag<WebSocket>();

        //public static async Task AddClient(string username, WebSocket client) {
        //    if (!_clients.ContainsKey(username)) {
        //        _clients.TryAdd(username, client);
        //    }
        //    //if (!_clients.Contains(client))
        //    //{
        //    //    Console.WriteLine("ellenorzes meghivva");
        //    //}
        //}
        public static async Task AddClient(WebSocket client)
        {
            _clients.Add(client);
        }

        //public static async Task NotifyClients(Models.Entities.Car newCar) { 
        //    foreach (var client in _clients)
        //    {
        //        if(client.State == WebSocketState.Open)
        //        {
        //            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newCar)));
        //            await client.SendAsync(buffer, WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        //        }
        //    }
        //}

        public static async Task NotifyClients(String message)
        {
            //foreach (var client in _clients.Values)
            //{
            //    if (client.State == WebSocketState.Open)
            //    {
            //        var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            //        await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            //    }
            //}

            foreach (var client in _clients)
            {
                if (client.State == WebSocketState.Open)
                {
                    var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    await client.SendAsync(buffer, WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
                }
            }
        }
    }
}
