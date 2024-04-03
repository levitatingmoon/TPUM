using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace Data
{
    internal class Item : IItem
    {

        public string name { get; set; }
        public float price { get; set; }
        public Guid id { get; set; }
        public ItemType type { get; set; }

        [JsonConstructor]
        public Item(string name, float price, Guid id, ItemType type) {

            this.name = name;
            this.price = price;
            this.id = id;
            this.type = type;

         
        }


        public Item(string name, float price, ItemType type)
        {
            this.name = name;
            this.price = price;
            this.type = type;

            this.id = Guid.NewGuid();
        }
    }
}
