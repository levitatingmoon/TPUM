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
        private IStorage shopInventory;
        private Discount discount;

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public Shop(IStorage shopInventory)
        {
            this.shopInventory = shopInventory;
            discount = new Discount(shopInventory);
            shopInventory.PriceChanged += OnPriceChanged;

        }

        public List<ShopItem> GetWeapons(bool onSale = true)
        {
            Tuple<Guid, float> sale = new Tuple<Guid, float>(Guid.Empty, 1f);
            if (onSale)
                sale = discount.GetDiscount();

            List<ShopItem> availableWeapons = new List<ShopItem>();

            foreach (IItem item in shopInventory.ItemList)
            {
                float price = item.price;
                if (item.id.Equals(sale.Item1))
                    price *= sale.Item2;

                availableWeapons.Add(new ShopItem
                {
                    Name = item.name,
                    Price = price,
                    Id = item.id,
                    Type = item.type.ToString()               
                });
            }

            return availableWeapons;
        }

        public bool Sell(List<ShopItem> items)
        {
            List<Guid> itemIDs = new List<Guid>();

            foreach (ShopItem item in items)
                itemIDs.Add(item.Id);

            List<IItem> weaponsDataLayer = shopInventory.GetItemsByID(itemIDs);

            shopInventory.RemoveItems(weaponsDataLayer);

            return true;
        }

        private void OnPriceChanged(object sender, Data.PriceChangedEventArgs e)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new Logic.PriceChangedEventArgs(e.Id, e.Price));
        }
    }
}
