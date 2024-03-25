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

        public List<ItemPresentation> GetWeapons()
        {
            List<ItemPresentation> weapons = new List<ItemPresentation>();
            foreach (ShopItem weapon in Shop.GetItems())
            {
                weapons.Add(new ItemPresentation(weapon.Name, weapon.Price, weapon.Id, weapon.Type));
            }
            return weapons;
        }

        public event EventHandler<Model.PriceChangedEventArgs> PriceChanged;
    }
}

