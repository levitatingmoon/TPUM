using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    public interface IItem
    {
        string Name { get; }
        float Price { get; set; }
        Guid Id { get; }
        ItemType Type { get; }

        public static IItem Create(string name, float price, ItemType type)
        {
            return new Item(name, price, type);
        }
    }
}
