using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    internal class ShoppingCart : IShoppingCart
    {
        private List<IShopItem> items;
        public float shoppingCartValue { get; private set; }


        public ShoppingCart()
        {
            items = new List<IShopItem>();
            shoppingCartValue = 0;
        }

        public void AddItem(IShopItem item)
        {
            items.Add(item);
            shoppingCartValue += item.Price;
        }
        public void RemoveItem(IShopItem item)
        {
            items.Remove(item);
            shoppingCartValue -= item.Price;
        }

    }
}
