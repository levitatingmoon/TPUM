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
       
        public event EventHandler<ShopItem> OnItemChanged;
        public event EventHandler<ShopItem> OnItemRemoved;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<ShopItem>> TransactionSucceeded;


        List<ShopItem> GetItems(bool onSale = true);
        public Task Sell(List<ShopItem> items);

        public Task SendMessageAsync(string message);

    }
}
