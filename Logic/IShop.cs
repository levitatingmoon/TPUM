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
       
        public event EventHandler<IShopItem> OnItemChanged;
        public event EventHandler<IShopItem> OnItemRemoved;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<IShopItem>> TransactionSucceeded;


        List<IShopItem> GetItems(bool onSale = true);
        public Task Sell(List<IShopItem> items);

        public Task SendMessageAsync(string message);

    }
}
