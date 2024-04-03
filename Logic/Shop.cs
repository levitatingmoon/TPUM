using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class Shop : IShop, IObserver<IItem>
    {
        private IStorage Storage;
        private Discount discount;
        private IDisposable unsubscriber;

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public event EventHandler<IShopItem> OnItemChanged;
        public event EventHandler<IShopItem> OnItemRemoved;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<IShopItem>> TransactionSucceeded;


        public Shop(IStorage storage)
        {
            this.Storage = storage;
            discount = new Discount(storage);
            storage.PriceChanged += OnPriceChanged;
            storage.TransactionFailed += OnTransactionFailed;
            storage.TransactionSucceeded += OnTransactionSucceeded;
            storage.Subscribe(this);
        }

        private void OnTransactionSucceeded(object? sender, List<IItem> e)
        {
            this.Storage.RemoveItems(e);
            EventHandler<List<IShopItem>> handler = TransactionSucceeded;
            List<IShopItem> soldItems = new List<IShopItem>();

            foreach (IItem item in e)
            {
                ShopItem shopItem = new ShopItem();
                shopItem.Type = item.Type.ToString();
                shopItem.Id = item.Id;
                shopItem.Name = item.Name;
                shopItem.Price = item.Price;

                soldItems.Add(shopItem);
            }

            handler?.Invoke(this, soldItems);
        }

        private void OnTransactionFailed(object? sender, EventArgs e)
        {
            EventHandler handler = TransactionFailed;
            handler?.Invoke(this, e);
        }


        public List<IShopItem> GetItems(bool isDiscounted = true)
        {
            Tuple<Guid, float> discount = new Tuple<Guid, float>(Guid.Empty, 1f);
            
            if (isDiscounted)
                discount = this.discount.GetDiscount();

            List<IShopItem> availableItems = new List<IShopItem>();

            foreach (IItem item in Storage.ItemList)
            {
                float price = item.Price;
                if (item.Id.Equals(discount.Item1))
                    price *= discount.Item2;

                availableItems.Add(new ShopItem
                {
                    Name = item.Name,
                    Price = price,
                    Id = item.Id,
                    Type = item.Type.ToString()               
                });
            }

            return availableItems;
        }

        public async Task Sell(List<IShopItem> items)
        {          
           List<Guid> itemIDs = new List<Guid>();

           foreach (IShopItem item in items)
               itemIDs.Add(item.Id);

            List<IItem> itemsDataLayer = Storage.GetItemsByID(itemIDs);


           foreach (IShopItem item in items)
            {
                IItem itemTemp = itemsDataLayer.Find(x => x.Id == item.Id);

                if (itemTemp != null)
                {
                    itemTemp.Price = item.Price;
                }
            }
                  
            await Storage.TryBuying(itemsDataLayer);
        }

        public async Task SendMessageAsync(string message)
        {
            await this.Storage.SendAsync(message);
        }

        private void OnPriceChanged(object sender, Data.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new Logic.PriceChangedEventArgs(e.Id, e.Price));
        }

        private void Unsunscribe()
        {
            unsubscriber.Dispose();
        }


        public void OnCompleted()
        {
            this.Unsunscribe();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IItem value)
        {
            ShopItem shopItem = new ShopItem();
            shopItem.Price = value.Price;
            shopItem.Id = value.Id;
            shopItem.Name = value.Name;
            shopItem.Type = value.Type.ToString();
        

            if (value.Price < -0.01f && value.Name == "")
                OnItemRemoved?.Invoke(this, shopItem);
            else
                OnItemChanged?.Invoke(this, shopItem);
        }
    }
}
