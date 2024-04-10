using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using System.Threading.Tasks;
using LogicServer;

namespace PresentationServer
{
    internal class Program
    {
        static IShop shop;
        private static LogicAbstractApi logicLayer;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Server started");
            logicLayer = LogicAbstractApi.Create();
            shop = logicLayer.Shop;
            shop.PriceChanged += async (sender, eventArgs) =>
            {
                if (WebSocketServer.CurrentConnection != null)
                    await SendMessageAsync("PriceChanged" + eventArgs.Price.ToString() + "/" + eventArgs.Id.ToString());
            };
            await WebSocketServer.Server(8081, ConnectionHandler);
        }

        static void ConnectionHandler(WebSocketConnection webSocketConnection)
        {
            Console.WriteLine("[Server]: Client connected");
            WebSocketServer.CurrentConnection = webSocketConnection;
            webSocketConnection.OnMessage = ParseMessage;
            webSocketConnection.OnClose = () => {
                Console.WriteLine("[Server]: Connection closed");
                WebSocketServer.CurrentConnection = null;
            };
            webSocketConnection.OnError = () => {
                Console.WriteLine("[Server]: Connection error encountered");
                WebSocketServer.CurrentConnection = null;
            };
        }

        static async void ParseMessage(string message)
        {
            Console.WriteLine($"[Client]: {message}");
            if (message == "main page button click")
                await SendMessageAsync("main page button click response");

            if (message == "RequestAll")
                await SendCurrentStorageState();

            if (message.Contains("RequestTransaction"))
            {
                var json = message.Substring("RequestTransaction".Length);
                List<IShopItem> itemsToBuy = Serializer.JSONToStorage(json);
                bool sellResult = shop.Sell(itemsToBuy);
                int sellResultInt = sellResult ? 1 : 0;

                await SendMessageAsync("TransactionResult" + sellResultInt.ToString() + (sellResult ? json : ""));
            }
        }

        static async Task SendCurrentStorageState()
        {
            var items = shop.GetItems();
            //var tempItems = items.Select(x=> x.ToDTO()).ToArray();
            var json = Serializer.StorageToJSON(items);
            var message = "UpdateAll" + json;

            await SendMessageAsync(message);
        }

        static async Task SendMessageAsync(string message)
        {
            Console.WriteLine("[Server]: " + message);
            await WebSocketServer.CurrentConnection.SendAsync(message);
        }
    }
}
