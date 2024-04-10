using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicServer;

namespace PresentationServer
{
    [Serializable]
    public class ShopItem : IShopItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public int Type { get; set; }

        public ShopItem(string name, float price, Guid id, int type)
        {
            Name = name;
            Price = price;
            Id = id;
            Type = type;
        }
    }
}
