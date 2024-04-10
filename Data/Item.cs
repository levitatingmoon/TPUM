using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;



namespace Data
{
    internal class Item : IItem
    {

        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public ItemType Type { get; set; }

        [JsonConstructor]
        public Item(string name, float price, Guid id, ItemType type) {

            Name = name;
            Price = price;
            Id = id;
            Type = type;

         
        }

        public Item(Guid id, string name, float price, ItemType type)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
        }


        public Item(string name, float price, ItemType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;  
            Type = type;
        }
    }
}
