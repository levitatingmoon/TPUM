using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IShop
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        List<ShopItem> GetItems(bool onSale = true);
        bool Sell(List<ShopItem> items);

    }
}
