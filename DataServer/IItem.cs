using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    public interface IItem
    {
        string name { get; }
        float price { get; set; }
        Guid id { get; }
        ItemType type { get; }

        public static IItem Create(string name, float price, ItemType type)
        {
            return new Item(name, price, type);
        }
    }
}
