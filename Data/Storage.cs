using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

   /*         ItemList.Add(new Item("Golden Delicious", 4.0f, ItemType.Apple));
            ItemList.Add(new Item("Red Delicious", 5.0f, ItemType.Apple));
            ItemList.Add(new Item("Premium Banana", 5.5f, ItemType.Banana));
            ItemList.Add(new Item("Yellow Carrots", 4.5f, ItemType.Carrot));
            ItemList.Add(new Item("Asian Pear", 6.0f, ItemType.Pear));
            ItemList.Add(new Item("European Pear", 3.5f, ItemType.Pear));
            ItemList.Add(new Item("Persian Cucumbers", 6.5f, ItemType.Cucumber));
*/
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
                foreach (ItemDTO item in items)          
                    AddItem(item.ToItem());                    
            }
        }

        public void RemoveItem(IItem item)
        {
            lock (lockObject)
            {
                IItem itemToRemove = ItemList.Find(x => x.Id == item.Id);
                if (itemToRemove == null)
                {
                    return;
                }

                ItemList.Remove(itemToRemove);

                itemToRemove.Name = "";
                itemToRemove.Price = -1f;
                itemToRemove.Type = ItemType.Removed;

                foreach (var observer in observers)
                    observer.OnNext(item);
            }
        }

        public void RemoveItems(List<IItem> items)
        {
            lock (lockObject)
            {
               foreach(var item in items)
                    RemoveItem(item);           
            }
        }

        public List<IItem> GetItemsOfType(ItemType type)
        {
            List<IItem> items = new List<IItem>();
            lock (lockObject)
            {
                foreach (IItem item in ItemList)
                {
                    if (item.Type == type)
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        private void UpdatePrice(PriceChangedResponse response)
        {
            if (response == null) 
                return;

            lock (lockObject) {

                ChangePrice(response.ItemID, response.Price);
       
            }

        }


        public void ChangePrice(Guid id, float newPrice)
        {
            IItem item = ItemList.Find(x => x.Id.Equals(id));
            lock (lockObject)
            {

                if (item == null)
                    return;

                if (Math.Abs(newPrice - item.Price) < 0.01f)
                    return;

                item.Price = newPrice;
                OnPriceChanged(item.Id, item.Price);
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
                    List<IItem> tmp = ItemList.FindAll(x => x.Id == guid);

                    if (tmp.Count > 0)
                        items.AddRange(tmp);
                }
            }

            return items;
        }

        public IItem CreateItem(string name, float price , ItemType type)
        {
            return new Item(name, price, type);
        }

        public async Task SendAsync(string mesg)
        {
            waitingForStorageUpdate = true;
            await WebSocketClient.CurrentConnection.SendAsync(mesg);
        }

        public async Task RequestItemsUpdate()
        {
            Serializer serializer = Serializer.Create();
            GetItemsCommand itemsCommand = new GetItemsCommand { Header = ServerStatics.GetItemsCommandHeader };

            await WebSocketClient.CurrentConnection.SendAsync(serializer.Serialize(itemsCommand));
        }

        private void UpdateAllProducts(UpdateAllResponse response)
        {
            if (response.Items != null)
            {
                lock (lockObject)
                {
                   ItemList.Clear();
                    foreach (ItemDTO item in response.Items)
                        AddItem(item.ToItem());
                }
            }
        }


        private void ParseMessage(string message)
        {
            Serializer serializer = Serializer.Create();

            if (serializer.GetResponseHeader(message) == ServerStatics.UpdateAllResponseHeader) { 
                UpdateAllResponse response = serializer.Deserialize<UpdateAllResponse>(message);
                UpdateAllProducts(response);
            }
            else if (serializer.GetResponseHeader(message) == ServerStatics.InflationChangedResponseHeader)
            {
                PriceChangedResponse response = serializer.Deserialize<PriceChangedResponse>(message);
                UpdatePrice(response);
            }
            else if (serializer.GetResponseHeader(message) == ServerStatics.TransactionResponseHeader)
            {
                TransactionResponse response = serializer.Deserialize<TransactionResponse>(message);
                if (response.Succeeded)
                {   
                    //ventHandler<List<IItem>> handler = TransactionSucceeded;
                    // handler?.Invoke(this, Serializer.JSONToStorage(resString.Substring(1)));
                    Task.Run(() => RequestItems());
                    //handler?.Invoke(this, e);
                    Console.WriteLine("response Git ");
                }
                else
                {
                    EventHandler handler = TransactionFailed;
                    handler?.Invoke(this, EventArgs.Empty);
                    RequestItemsUpdate();
                }
            }

                waitingForSellResponse = false;
        }

        public async Task RequestItems()
        {
            Serializer serializer = Serializer.Create();
            GetItemsCommand itemsCommand = new GetItemsCommand { Header = ServerStatics.GetItemsCommandHeader };
            await SendAsync(serializer.Serialize(itemsCommand));
        }


        private async void Connected()
        {
            WebSocketClient.CurrentConnection.onMessage = ParseMessage;
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
            List<Guid> ids = new List<Guid>();

            foreach (IItem item in items)
            {
                ids.Add(item.Id);
            }

            Serializer serializer = Serializer.Create();
            SellItemCommand sellItemCommand = new SellItemCommand
            {
                Header = ServerStatics.SellItemCommandHeader,
                Items = ids
            };
            await SendAsync(serializer.Serialize(sellItemCommand));

            waitingForSellResponse = true;
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
