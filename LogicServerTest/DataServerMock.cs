﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServer;

namespace LogicServerTest
{
    internal class DataServerLayerTest : DataAbstractApi
    {
        private readonly IStorage storage = new StorageMock();
        public override IStorage Storage
        {
            get { return storage; }
            set { }
        }
    }

    internal class StorageMock : IStorage
    {
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public List<IItem> ItemList { get; }

        public StorageMock()
        {
            ItemList = new List<IItem>
            {
                new ItemMock("Golden Delicious", 5.5f,ItemType.Apple),
                new ItemMock("Premium Banana", 5.5f,ItemType.Banana),
            };
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
            return ItemList.FindAll(item => item.Type == type);
        }

        public void ChangePrice(Guid id, float newPrice)
        {
            IItem item = ItemList.Find(x => x.Id.Equals(id));

            if (item != null)
                return;

            if (Math.Abs(newPrice - item.Price) < 0.01f)
                return;

            item.Price = newPrice;
            OnPriceChanged(item.Id, item.Price);
        }

        private void OnPriceChanged(Guid id, float price)
        {
            EventHandler<PriceChangedEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new PriceChangedEventArgs(id, price));
        }

        public List<IItem> GetItemsByID(List<Guid> Ids)
        {
            List<IItem> items = new List<IItem>();
            foreach (Guid guid in Ids)
            {
                List<IItem> tmp = ItemList.FindAll(x => x.Id == guid);

                if (tmp.Count > 0)
                    items.AddRange(tmp);
            }

            return items;
        }
    }

    internal class ItemMock : IItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public ItemType Type { get; set; }

        public ItemMock(string name, float price, ItemType type)
        {
            this.Name = name;
            this.Price = price;
            this.Type = type;

            this.Id = Guid.NewGuid();
        }

    }
}
