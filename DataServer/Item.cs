using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    internal class Item : IItem
    {

        public string name { get; set; }
        public float price { get; set; }
        public Guid id { get; set; }
        public ItemType type { get; set; }

        public Item(string name, float price, ItemType type)
        {
            this.name = name;
            this.price = price;
            this.type = type;

            this.id = Guid.NewGuid();
        }
    }
}
