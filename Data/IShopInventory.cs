using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IShopInventory
    {
        public List<IItem> ItemList { get; }
        public void AddItem(IItem item);

        public void AddItems(List<IItem> items);
        public void RemoveItems(List<IItem> items);
        public List<IItem> GetItemsOfType(ItemType type);
    }
}
