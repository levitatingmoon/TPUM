using Data;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {

        private IStorage inventory;

        [TestInitialize]
        public void TestMethod()
        {

            inventory = DataAbstractApi.Create().Storage;
            Assert.IsNotNull(inventory);

            inventory.ItemList.Clear();
            Assert.AreEqual(0, inventory.ItemList.Count);

            List<IItem> items = new List<IItem> {

                new Item("small carrot", 2.0f, ItemType.Carrot),
                new Item("cucumber", 3.0f, ItemType.Cucumber),
            };

            inventory.AddItems(items);

        }

        [TestMethod]
        public void AddItemTest()
        {

            Item item = new Item("golden delicious", 5.0f, ItemType.Apple);
            inventory.AddItem(item);
            Assert.AreEqual(3, inventory.ItemList.Count);
        }

        [TestMethod]
        public void AddItemsTest()
        {

            List<IItem> items = new List<IItem>();
            items.Add(new Item("carrot", 4.0f, ItemType.Carrot));
            items.Add(new Item("banana", 3.0f, ItemType.Banana));
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

    }
}