using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IStorage
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public List<IItem> ItemList { get; }
        public void AddItem(IItem item);

        public void AddItems(List<IItem> items);
        public void RemoveItems(List<IItem> items);
        public void ChangePrice(Guid id, float newPrice);
        public List<IItem> GetItemsOfType(ItemType type);

        public List<IItem> GetItemsByID(List<Guid> Ids);

    }
}
