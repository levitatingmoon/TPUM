using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IStorage : IObservable<IItem>
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<IItem>> TransactionSucceeded;


        public List<IItem> ItemList { get; }
        public void AddItem(IItem item);

        public IItem CreateItem(string name, float price, ItemType type);

        public void AddItems(List<IItem> items);
        public void RemoveItem(IItem item);
        public void RemoveItems(List<IItem> items);
        public void ChangePrice(Guid id, float newPrice);
        public List<IItem> GetItemsOfType(ItemType type);

        public List<IItem> GetItemsByID(List<Guid> Ids);

        public Task SendAsync(string mesg);
        public Task RequestItemsUpdate();
        Task TryBuying(List<IItem> items);
    }
}
