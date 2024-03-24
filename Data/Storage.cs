using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Storage : IStorage
    {
        public event EventHandler<PriceChangedEventArgs> PriceChange;
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public List<IItem> ItemList { get; }

        public Storage()
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

        public void ChangePrice(Guid id, float newPrice)
        {
            IItem item = ItemList.Find(x => x.id.Equals(id));

            if (item != null)
                return;

            if (Math.Abs(newPrice - item.price) < 0.01f)
                return;

            item.price = newPrice;
            OnPriceChanged(item.id, item.price);
        }

        private void OnPriceChanged(Guid id, float price)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChange;
            handler?.Invoke(this, new PriceChangedEventArgs(id, price));
        }

        public List<IItem> GetItemsByID(List<Guid> Ids)
        {
            List<IItem> items = new List<IItem>();
            foreach (Guid guid in Ids)
            {
                List<IItem> tmp = ItemList.FindAll(x => x.id == guid);

                if (tmp.Count > 0)
                    items.AddRange(tmp);
            }

            return items;
        }
    }
}
