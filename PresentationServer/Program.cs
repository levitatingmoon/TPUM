using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using System.Threading.Tasks;
using LogicServer;
using System.Diagnostics.Tracing;

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
                if (WebSocketServer.CurrentConnection == null)
                    return;

                Console.WriteLine("Obnizka: " + eventArgs.Price);

                List<IShopItem> items = logicLayer.Shop.GetItems();
                PriceChangedResponse response = new PriceChangedResponse();
                response.Price = eventArgs.Price;
                response.ItemID  = eventArgs.Id;
               
                Serializer serializer = Serializer.Create();
                string responseJson = serializer.Serialize(response);
                Console.WriteLine(responseJson);

                await SendMessageAsync(responseJson);


                    //await SendMessageAsync("PriceChanged" + eventArgs.Price.ToString() + "/" + eventArgs.Id.ToString());
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

            Serializer serializer = Serializer.Create();

            if (serializer.GetCommandHeader(message) == GetItemsCommand.StaticHeader)
            {
                GetItemsCommand getItemsCommand = serializer.Deserialize<GetItemsCommand>(message);
                Task task = Task.Run(async () => await SendCurrentStorageState());
            }
            else if (serializer.GetCommandHeader(message) == SellItemCommand.StaticHeader)
            {
                SellItemCommand sellItemCommand = serializer.Deserialize<SellItemCommand>(message);

                TransactionResponse transactionResponse = new TransactionResponse();
                
                try
                {
                    Console.WriteLine("Sprzedanooo!!!");
                    shop.SellItems(sellItemCommand.Items);
                    transactionResponse.Succeeded = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception \"{exception.Message}\" caught during selling item");
                    transactionResponse.Succeeded = false;
                }

                string transactionMessage = serializer.Serialize(transactionResponse);
                Console.WriteLine($"Send: {transactionMessage}");
                await SendMessageAsync(transactionMessage);
            }
        }

        static async Task SendCurrentStorageState()
        {
            UpdateAllResponse response = new UpdateAllResponse();
            List<IShopItem> items = shop.GetItems();
            response.Items = items.Select(x => x.ToDTO()).ToArray(); 
            
            Serializer serializer = Serializer.Create();
            string responseJson = serializer.Serialize(response);
            Console.WriteLine("Tutaj jest response Json: " + responseJson);
            await SendMessageAsync(responseJson);
        }


        static async Task SendMessageAsync(string message)
        {
            Console.WriteLine("[Server]: " + message);
            await WebSocketServer.CurrentConnection.SendAsync(message);
        }
    }
}
