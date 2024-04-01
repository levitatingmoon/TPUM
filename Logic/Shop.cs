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

        public event EventHandler<ShopItem> OnItemChanged;
        public event EventHandler<ShopItem> OnItemRemoved;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<ShopItem>> TransactionSucceeded;


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
            EventHandler<List<ShopItem>> handler = TransactionSucceeded;
            List<ShopItem> soldItems = new List<ShopItem>();

            foreach (IItem item in e)
            {
                ShopItem shopItem = new ShopItem();
                shopItem.Type = item.type.ToString();
                shopItem.Id = item.id;
                shopItem.Name = item.name;
                shopItem.Price = item.price;

                soldItems.Add(shopItem);
            }

            handler?.Invoke(this, soldItems);
        }

        private void OnTransactionFailed(object? sender, EventArgs e)
        {
            EventHandler handler = TransactionFailed;
            handler?.Invoke(this, e);
        }


        public List<ShopItem> GetItems(bool isDiscounted = true)
        {
            Tuple<Guid, float> discount = new Tuple<Guid, float>(Guid.Empty, 1f);
            
            if (isDiscounted)
                discount = this.discount.GetDiscount();

            List<ShopItem> availableItems = new List<ShopItem>();

            foreach (IItem item in Storage.ItemList)
            {
                float price = item.price;
                if (item.id.Equals(discount.Item1))
                    price *= discount.Item2;

                availableItems.Add(new ShopItem
                {
                    Name = item.name,
                    Price = price,
                    Id = item.id,
                    Type = item.type.ToString()               
                });
            }

            return availableItems;
        }

        public async Task Sell(List<ShopItem> items)
        {          
           List<Guid> itemIDs = new List<Guid>();

           foreach (ShopItem item in items)
               itemIDs.Add(item.Id);

            List<IItem> itemsDataLayer = Storage.GetItemsByID(itemIDs);


           foreach (ShopItem item in items)
            {
                IItem itemTemp = itemsDataLayer.Find(x => x.id == item.Id);

                if (itemTemp != null)
                {
                    itemTemp.price = item.Price;
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
            shopItem.Price = value.price;
            shopItem.Id = value.id;
            shopItem.Name = value.name;
            shopItem.Type = value.type.ToString();
        

            if (value.price < -0.01f && value.name == "")
                OnItemRemoved?.Invoke(this, shopItem);
            else
                OnItemChanged?.Invoke(this, shopItem);
        }
    }
}
