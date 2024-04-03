using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IShoppingCart
    {
        void AddItem(IShopItem item);
        void RemoveItem(IShopItem item);
    }
}
