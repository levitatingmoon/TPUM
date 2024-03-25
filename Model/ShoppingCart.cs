using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ShoppingCart
    {
        public ObservableCollection<ItemPresentation> Items { get; set; }
        private IShop Shop { get; set; }

        public ShoppingCart(ObservableCollection<ItemPresentation> items, IShop shop)
        {
            Items = items;
            Shop = shop;
        }

        public void Add(ItemPresentation item)
        {
            Items.Add(item);
        }

        public float Sum()
        {
            float res = 0f;
            foreach (ItemPresentation item in Items)
            {
                res += item.Price;
            }

            return res;
        }

        public bool Buy()
        {
            List<ShopItem> shoppingList = new List<ShopItem>();


            foreach (ItemPresentation itemPresentation in Items)
            {
                shoppingList.Add(Shop.GetItems().FirstOrDefault(x => x.Id == itemPresentation.Id));
            }

            bool res = Shop.Sell(shoppingList);
            if (res)
            {
                Items.Clear();
            }

            return res;
        }
    }
}
