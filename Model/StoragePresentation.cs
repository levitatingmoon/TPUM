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
        public event EventHandler<IItemPresentation> ItemChanged;
        public event EventHandler<IItemPresentation> ItemRemoved;
        public event EventHandler<List<IItemPresentation>> TransactionSucceeded;
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

        public List<IItemPresentation> GetItems()
        {
            List<IItemPresentation> items = new List<IItemPresentation>();
            foreach (IShopItem item in Shop.GetItems())
            {
                items.Add(CreateItem(item.Name, item.Price, item.Id, item.Type));
            }
            return items;
        }

        public async Task SendMessageAsync(string mesg)
        {
            await Shop.SendMessageAsync(mesg);
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

        private void OnItemRemoved(object? sender, IShopItem e)
        {
            EventHandler<IItemPresentation> handler = ItemRemoved;
            IItemPresentation Item = CreateItem(e.Name, e.Price, e.Id, e.Type);
            handler?.Invoke(this, Item);
        }

        private void OnTransactionSucceeded(object? sender, List<IShopItem> e)
        {
            EventHandler<List<IItemPresentation>> handler = TransactionSucceeded;
            List<IItemPresentation> soldItemPresentations = new List<IItemPresentation>();
            foreach (IShopItem shopItem in e)
            {
                IItemPresentation ItemPresentation = CreateItem(shopItem.Name, shopItem.Price, shopItem.Id,
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

        private void OnItemChanged(object? sender, IShopItem e)
        {
            EventHandler<IItemPresentation> handler = ItemChanged;
            IItemPresentation item = CreateItem(e.Name, e.Price, e.Id, e.Type);
            handler?.Invoke(this, item);
        }

        public IItemPresentation CreateItem(string name, float price, Guid id, string type)
        {
            return new ItemPresentation(name, price, id, type);
        }

        private void OnPriceChanged(object sender, Logic.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new PriceChangedEventArgs(e.Id, e.Price));
        }
    }
}

