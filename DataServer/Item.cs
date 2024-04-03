using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    internal class Item : IItem
    {

        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public ItemType Type { get; set; }

        public Item(string name, float price, ItemType type)
        {
            Name = name;
            Price = price;
            Type = type;

            Id = Guid.NewGuid();
        }
    }
}
