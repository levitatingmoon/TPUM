using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    internal class ShoppingCart : IShoppingCart
    {
        private List<ShopItem> items;
        public float shoppingCartValue { get; private set; }


        public ShoppingCart()
        {
            items = new List<ShopItem>();
            shoppingCartValue = 0;
        }

        public void AddItem(ShopItem item)
        {
            items.Add(item);
            shoppingCartValue += item.Price;
        }
        public void RemoveItem(ShopItem item)
        {
            items.Remove(item);
            shoppingCartValue -= item.Price;
        }

    }
}
