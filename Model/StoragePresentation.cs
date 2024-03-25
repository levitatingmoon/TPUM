using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StoragePresentation
    {
        private IShop Shop { get; set; }

        public StoragePresentation(IShop shop)
        {
            Shop = shop;
            Shop.PriceChanged += OnPriceChanged;
        }
        private void OnPriceChanged(object sender, Logic.PriceChangedEventArgs e)
        {
            EventHandler<Model.PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new Model.PriceChangedEventArgs(e.Id, e.Price));
        }

        public List<ItemPresentation> GetItems()
        {
            List<ItemPresentation> items = new List<ItemPresentation>();
            foreach (ShopItem item in Shop.GetItems())
            {
                items.Add(new ItemPresentation(item.Name, item.Price, item.Id, item.Type));
            }
            return items;
        }

        public event EventHandler<Model.PriceChangedEventArgs> PriceChanged;
    }
}

