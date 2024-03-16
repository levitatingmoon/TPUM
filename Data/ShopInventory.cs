using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class ShopInventory : IShopInventory
    {
        public List<IItem> ItemList { get; }

        public ShopInventory()
        {
            ItemList = new List<IItem>();

            ItemList.Add(new Item("golden delicious", 5.0f, ItemType.Apple));
            ItemList.Add(new Item("premium banana", 5.5f, ItemType.Banana));

        }

        public void AddItem(IItem item)
        {
            ItemList.Add(item);
        }

        public void AddItems(List<IItem> items)
        {
            ItemList.AddRange(items);
        }

        public void RemoveItems(List<IItem> items)
        {
            items.ForEach(item => ItemList.Remove(item));
        }

        public List<IItem> GetItemsOfType(ItemType type)
        {
            return ItemList.FindAll(item => item.type == type);
        }

    }
}
