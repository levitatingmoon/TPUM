using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StoragePresentation
    {
        private IShop Shop { get; set; }
        private IConnectionService ConnectionService;

        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        public event EventHandler<ItemPresentation> ItemChanged;
        public event EventHandler<ItemPresentation> ItemRemoved;
        public event EventHandler<List<ItemPresentation>> TransactionSucceeded;
        public event EventHandler TransactionFailed;

        public StoragePresentation(IShop shop)
        {
            Shop = shop;
            Shop.PriceChanged += OnPriceChanged;
            Shop.OnItemChanged += OnItemChanged;
            Shop.TransactionFailed += OnTransactionFailed;
            Shop.OnItemRemoved += OnItemRemoved;
            Shop.TransactionSucceeded += OnTransactionSucceeded;
            ConnectionService = ServiceFactory.CreateConnectionService;
            ConnectionService.ConnectionLogger += ConnectionLogger;
        }

        public List<ItemPresentation> GetItems()
        {
            List<ItemPresentation> items = new List<ItemPresentation>();
            foreach (ShopItem item in Shop.GetItems())
            {
                items.Add(new ItemPresentation(item.Name, item.Price, item.Id, item.Type));
            }
            return items;
        }

        public async Task SendMessageAsync(string mesg)
        {
            Shop.SendMessageAsync(mesg);
        }

        public Task<bool> Connect(Uri uri)
        {
            return ConnectionService.Connect(uri);
        }

        public async Task Disconnect()
        {
            await ConnectionService.Disconnect();
        }

        public bool IsConnected()
        {
            return ConnectionService.Connected;
        }


        private void ConnectionLogger(string mesg)
        {
            Console.WriteLine(mesg);
        }

        private void OnItemRemoved(object? sender, ShopItem e)
        {
            EventHandler<ItemPresentation> handler = ItemRemoved;
            ItemPresentation Item = new ItemPresentation(e.Name, e.Price, e.Id, e.Type);
            handler?.Invoke(this, Item);
        }

        private void OnTransactionSucceeded(object? sender, List<ShopItem> e)
        {
            EventHandler<List<ItemPresentation>> handler = TransactionSucceeded;
            List<ItemPresentation> soldItemPresentations = new List<ItemPresentation>();
            foreach (ShopItem shopItem in e)
            {
                ItemPresentation ItemPresentation = new ItemPresentation(shopItem.Name, shopItem.Price, shopItem.Id,
                     shopItem.Type);
                soldItemPresentations.Add(ItemPresentation);
            }

            handler?.Invoke(this, soldItemPresentations);
        }

        private void OnTransactionFailed(object? sender, EventArgs e)
        {
            EventHandler handler = TransactionFailed;
            handler?.Invoke(this, e);
        }

        private void OnItemChanged(object? sender, ShopItem e)
        {
            EventHandler<ItemPresentation> handler = ItemChanged;
            ItemPresentation item = new ItemPresentation(e.Name, e.Price, e.Id, e.Type);
            handler?.Invoke(this, item);
        }

        private void OnPriceChanged(object sender, Logic.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new PriceChangedEventArgs(e.Id, e.Price));
        }
    }
}

