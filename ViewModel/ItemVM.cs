using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    internal class ItemVM : IItemVM
    {
        public ItemVM(string name, float price, Guid id, string itemType)
        {
            Name = name;
            Price = price;
            Id = id;
            Type = itemType;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
    }

    public interface IItemVM
    {

        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
