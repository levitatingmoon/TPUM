using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IItem
    {
        string Name { get; set; }
        float Price { get; set; }
        Guid Id { get; }
        ItemType Type { get; }

        public static IItem Create(string name, float price, ItemType type)
        {
            return new Item(name, price, type);
        }
    }
}
