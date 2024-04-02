using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServer;

namespace LogicServer
{
    public interface IShop
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        List<IShopItem> GetItems(bool onSale = true);
        bool Sell(List<IShopItem> items);

    }
}
