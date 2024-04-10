using Data;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {

        private static DataAbstractApi Create()
        {
            return new DataMock();
        }

        [TestMethod]
        public void StorageItemsTest()
        {
            DataAbstractApi Data = Create();
            List<IItem> items = Data.Storage.ItemList;
            Assert.AreEqual(2, items.Count);
        }

        [TestMethod]
        public void AddItemTest()
        {
            DataAbstractApi Data = Create();
            IItem item = Data.Storage.CreateItem("Pear", 3.0f, ItemType.Pear);
            Data.Storage.AddItem(item);
            Assert.AreEqual(3, Data.Storage.ItemList.Count);
        }

        [TestMethod]
        public void AddItemsTest()
        {
            DataAbstractApi Data = Create();
            IItem item = Data.Storage.CreateItem("Pear", 3.0f, ItemType.Pear);
            IItem item2 = Data.Storage.CreateItem("Carrot", 5.0f, ItemType.Carrot);
            List<IItem> items = new List<IItem>();
            items.Add(item);
            items.Add(item2);
            Data.Storage.AddItems(items);
            Assert.AreEqual(4, Data.Storage.ItemList.Count);
        }

        [TestMethod]
        public void GetItemsTest()
        {
            DataAbstractApi Data = Create();
            List<IItem> items = Data.Storage.GetItemsOfType(ItemType.Banana);
            Assert.IsNotNull(items);
            Assert.AreEqual(items.Count, 1);

        }

        [TestMethod]
        public void RemoveItemsTest()
        {
            DataAbstractApi Data = Create();
            List<IItem> toRemove = Data.Storage.GetItemsOfType(ItemType.Apple);
            Assert.IsNotNull(toRemove);
            Assert.AreEqual(1, toRemove.Count);
            Data.Storage.RemoveItems(toRemove);
            Assert.AreEqual(1, Data.Storage.ItemList.Count);
        }

    }

}