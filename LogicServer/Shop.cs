using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServer;

namespace LogicServer
{
    internal class Shop : IShop
    {
        private IStorage Storage;
        private Discount discount;

        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        private object objectLock = new object();

        public Shop(IStorage storage)
        {
            this.Storage = storage;
            discount = new Discount(storage);
            storage.PriceChanged += OnPriceChanged;

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
                    Type = (int)item.Type
                });
            }

            return availableItems;
        }

        public bool Sell(List<IShopItem> items)
        {
            lock (objectLock)
            {
                List<Guid> itemIDs = new List<Guid>();

                foreach (IShopItem item in items)
                    itemIDs.Add(item.Id);

                List<IItem> itemsDataLayer = Storage.GetItemsByID(itemIDs);

                Storage.RemoveItems(itemsDataLayer);

            }
            return true;
        }

        private void OnPriceChanged(object sender, DataServer.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new LogicServer.PriceChangedEventArgs(e.Id, e.Price));
        }
    }
}
