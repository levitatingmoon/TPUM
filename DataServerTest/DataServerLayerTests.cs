using DataServer;

namespace DataServerTest
{
    [TestClass]
    public class DataServerLayerTests
    {
        private IStorage inventory;

        [TestInitialize]
        public void TestMethod()
        {

            inventory = DataAbstractApi.Create().Storage;
            Assert.IsNotNull(inventory);

            inventory.ItemList.Clear();
            Assert.AreEqual(0, inventory.ItemList.Count);

            List<IItem> items = new List<IItem>();
            IItem item1 = IItem.Create("small carrot", 2.0f, ItemType.Carrot);
            IItem item2 = IItem.Create("cucumber", 3.0f, ItemType.Cucumber);
            items.Add(item1);
            items.Add(item2);

            inventory.AddItems(items);

        }

        [TestMethod]
        public void AddItemTest()
        {

            IItem item = IItem.Create("golden delicious", 5.0f, ItemType.Apple);
            inventory.AddItem(item);
            Assert.AreEqual(3, inventory.ItemList.Count);
        }

        [TestMethod]
        public void AddItemsTest()
        {

            List<IItem> items = new List<IItem>();
            items.Add(IItem.Create("carrot", 4.0f, ItemType.Carrot));
            items.Add(IItem.Create("banana", 3.0f, ItemType.Banana));
            inventory.AddItems(items);
            Assert.AreEqual(4, inventory.ItemList.Count);

        }

        [TestMethod]
        public void GetItemsTest()
        {


            List<IItem> carrots = inventory.GetItemsOfType(ItemType.Carrot);
            Assert.IsNotNull(carrots);
            Assert.AreEqual(1, carrots.Count);
            Assert.AreEqual(ItemType.Carrot, carrots[0].type);

        }

        [TestMethod]
        public void RemoveItemsTest()
        {

            List<IItem> toRemove = inventory.GetItemsOfType(ItemType.Cucumber);
            Assert.IsNotNull(toRemove);
            Assert.AreEqual(1, toRemove.Count);
            inventory.RemoveItems(toRemove);
            Assert.AreEqual(1, inventory.ItemList.Count);
        }

        [TestMethod]
        public void ChangeItemPriceTest()
        {
            IItem item = inventory.GetItemsOfType(ItemType.Cucumber)[0];
            Assert.IsNotNull(item);
            Assert.AreEqual(item.price, 3.0f);

            inventory.ChangePrice(item.id, 5.0f);
            Assert.AreEqual(item.price, 5.0f);
        }
    }
}
