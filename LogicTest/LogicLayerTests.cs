using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {
        private IStorage storage;
        private IShop Shop;

        [TestInitialize]
        public void TestMethod()
        {
            storage = IDataLayer.Create().Storage;
            Assert.IsNotNull(storage);

            storage.ItemList.Clear();
            Assert.AreEqual(0, storage.ItemList.Count);

            Shop = ILogicLayer.Create().Shop;
            Assert.IsNotNull(Shop);

        }

        [TestMethod]
        public void GetItemsTest()
        {
            Assert.AreEqual(0, storage.ItemList.Count);

            List<IItem> list = new List<IItem>()
            {
                new Item("Gruszka", 4f, ItemType.Pear),
                new Item("Marchewka Zielona", 2f, ItemType.Carrot),
                new Item("Marchewka Zielona", 2f, ItemType.Carrot)
            };


            storage.AddItems(list);
            Assert.AreEqual(3, storage.ItemList.Count);

            List<ShopItem> items = Shop.GetItems();
            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count);

        }
    }
}