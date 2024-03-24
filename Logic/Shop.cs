using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Shop : IShop
    {
        private IStorage Storage;
        private Discount discount;

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public Shop(IStorage storage)
        {
            this.Storage = storage;
            discount = new Discount(storage);
            storage.PriceChanged += OnPriceChanged;

        }

        public List<ShopItem> GetItems(bool onSale = true)
        {
            Tuple<Guid, float> sale = new Tuple<Guid, float>(Guid.Empty, 1f);
            
            if (onSale)
                sale = discount.GetDiscount();

            List<ShopItem> availableItems = new List<ShopItem>();

            foreach (IItem item in Storage.ItemList)
            {
                float price = item.price;
                if (item.id.Equals(sale.Item1))
                    price *= sale.Item2;

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

        public bool Sell(List<ShopItem> items)
        {
            List<Guid> itemIDs = new List<Guid>();

            foreach (ShopItem item in items)
                itemIDs.Add(item.Id);

            List<IItem> itemsDataLayer = Storage.GetItemsByID(itemIDs);

            Storage.RemoveItems(itemsDataLayer);

            return true;
        }

        private void OnPriceChanged(object sender, Data.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new Logic.PriceChangedEventArgs(e.Id, e.Price));
        }
    }
}
