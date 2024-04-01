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
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        public event EventHandler TransactionFailed;
        public event EventHandler<List<IItem>> TransactionSucceeded;

        private bool waitingForStorageUpdate;
        private bool waitingForSellResponse;
        private bool transactionSuccess;
        private List<IObserver<IItem>> observers;

        private object lockObject = new object();
        public List<IItem> ItemList { get; }

        public Storage()
        {
            ItemList = new List<IItem>();

            observers = new List<IObserver<IItem>>();

            waitingForStorageUpdate = false;
            waitingForSellResponse = false;
            transactionSuccess = false;

            ItemList.Add(new Item("Golden Delicious", 4.0f, ItemType.Apple));
            ItemList.Add(new Item("Red Delicious", 5.0f, ItemType.Apple));
            ItemList.Add(new Item("Premium Banana", 5.5f, ItemType.Banana));
            ItemList.Add(new Item("Yellow Carrots", 4.5f, ItemType.Carrot));
            ItemList.Add(new Item("Asian Pear", 6.0f, ItemType.Pear));
            ItemList.Add(new Item("European Pear", 3.5f, ItemType.Pear));
            ItemList.Add(new Item("Persian Cucumbers", 6.5f, ItemType.Cucumber));

            WebSocketClient.OnConnected += Connected;
        }

        public void AddItem(IItem item)
        {
            lock (lockObject)
            {
                ItemList.Add(item);

                foreach (var observer in observers)
                    observer.OnNext(item);
            }
        }

        public void AddItems(List<IItem> items)
        {
            lock (lockObject)
            {
                foreach (var item in items)          
                    AddItem(item);                    
            }
        }

        public void RemoveItems(List<IItem> items)
        {
            lock (lockObject)
            {
                items.ForEach(item => {
                    ItemList.Remove(item);

                    foreach (var observer in observers)
                        observer.OnNext(item);
                });

               
            }
        }

        public List<IItem> GetItemsOfType(ItemType type)
        {
            List<IItem> items = new List<IItem>();
            lock (lockObject)
            {
                foreach (IItem item in ItemList)
                {
                    if (item.type == type)
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public void ChangePrice(Guid id, float newPrice)
        {
            IItem item = ItemList.Find(x => x.id.Equals(id));
            lock (lockObject)
            {

                if (item == null)
                    return;

                if (Math.Abs(newPrice - item.price) < 0.01f)
                    return;

                item.price = newPrice;
                OnPriceChanged(item.id, item.price);
            }
        }

        private void OnPriceChanged(Guid id, float price)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new PriceChangedEventArgs(id, price));
        }

        public List<IItem> GetItemsByID(List<Guid> Ids)
        {
            List<IItem> items = new List<IItem>();

            lock (lockObject)
            {
                foreach (Guid guid in Ids)
                {
                    List<IItem> tmp = ItemList.FindAll(x => x.id == guid);

                    if (tmp.Count > 0)
                        items.AddRange(tmp);
                }
            }

            return items;
        }

        public async Task SendAsync(string mesg)
        {
            waitingForStorageUpdate = true;
            await WebSocketClient.CurrentConnection.SendAsync(mesg);
        }

        public async Task RequestItemsUpdate()
        {
            await WebSocketClient.CurrentConnection.SendAsync("RequestAll");
        }

        private void ParseMessage(string message)
        {
            if (message.Contains("UpdateAll"))
            {
                string json = message.Substring("UpdateAll".Length);
                List<IItem> items = Serializer.JSONToStorage(json);
                foreach (var item in items)
                    AddItem(item);
                waitingForStorageUpdate = false;
            }
            else if (message.Contains("TransactionResult"))
            {
                string resString = message.Substring("TransactionResult".Length);
                transactionSuccess = resString[0] == '1';

                if (!transactionSuccess)
                {
                    EventHandler handler = TransactionFailed;
                    handler?.Invoke(this, EventArgs.Empty);
                    RequestItemsUpdate();
                }
                else
                {
                    EventHandler<List<IItem>> handler = TransactionSucceeded;
                    handler?.Invoke(this, Serializer.JSONToStorage(resString.Substring(1)));
                }

                waitingForSellResponse = false;
            }
            else if (message.Contains("PriceChanged"))
            {
                string priceChangedStr = message.Substring("PriceChanged".Length);
                string[] parts = priceChangedStr.Split("/");
                ChangePrice(Guid.Parse(parts[1]), float.Parse(parts[0]));
            }
        }

        private async void Connected()
        {
           // WebSocketClient.CurrentConnection.OnMessage = ParseMessage;
            await RequestItemsUpdate();
        }

        public IDisposable Subscribe(IObserver<IItem> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public async Task TryBuying(List<IItem> items)
        {
            waitingForSellResponse = true;
            string json = Serializer.StorageToJSON(items);
            await WebSocketClient.CurrentConnection.SendAsync("RequestTransaction" + json);
        }


        private class Unsubscriber : IDisposable
        {
            private List<IObserver<IItem>> _observers;
            private IObserver<IItem> _observer;

            public Unsubscriber(List<IObserver<IItem>> observers, IObserver<IItem> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }




    }
}
