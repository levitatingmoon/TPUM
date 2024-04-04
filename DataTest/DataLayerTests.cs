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

            List<IItem> items = new List<IItem>();
            IItem item1 = inventory.CreateItem("small carrot", 2.0f, ItemType.Carrot);
            IItem item2 = inventory.CreateItem("cucumber", 3.0f, ItemType.Cucumber);
            items.Add(item1);
            items.Add(item2);

            inventory.AddItems(items);

        }

        [TestMethod]
        public void AddItemTest()
        {

            IItem item = inventory.CreateItem("golden delicious", 5.0f, ItemType.Apple);
            inventory.AddItem(item);
            Assert.AreEqual(3, inventory.ItemList.Count);
        }

        [TestMethod]
        public void AddItemsTest()
        {

            List<IItem> items = new List<IItem>();
            items.Add(inventory.CreateItem("carrot", 4.0f, ItemType.Carrot));
            items.Add(inventory.CreateItem("banana", 3.0f, ItemType.Banana));
            inventory.AddItems(items);
            Assert.AreEqual(4, inventory.ItemList.Count);

        }

        [TestMethod]
        public void GetItemsTest()
        {


            List<IItem> carrots = inventory.GetItemsOfType(ItemType.Carrot);
            Assert.IsNotNull(carrots);
            Assert.AreEqual(1, carrots.Count);
            Assert.AreEqual(ItemType.Carrot, carrots[0].Type);

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
            Assert.AreEqual(item.Price, 3.0f);

            inventory.ChangePrice(item.Id, 5.0f);
            Assert.AreEqual(item.Price, 5.0f);
        }
        
    }

}