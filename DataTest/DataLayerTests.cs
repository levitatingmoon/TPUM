using Data;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {

        private IStorage? inventory;

        [TestInitialize]
        public void TestMethod()
        {

            inventory = IDataLayer.Create().shopInventory;
            Assert.IsNotNull(inventory);

            inventory.ItemList.Clear();
            Assert.AreEqual(0, inventory.ItemList.Count);

        }

        [TestMethod]
        public void ShopInventoryTests()
        {

            Item item = new Item("golden delicious", 5.0f, ItemType.Apple);
            inventory.AddItem(item);
            Assert.AreEqual(1, inventory.ItemList.Count);



            List<IItem> items = new List<IItem>();
            items.Add(new Item("small carrot", 2.0f, ItemType.Carrot));
            items.Add(new Item("cucumber", 3.0f, ItemType.Cucumber));
            inventory.AddItems(items);
            Assert.AreEqual(3, inventory.ItemList.Count);



            List<IItem> carrots = inventory.GetItemsOfType(ItemType.Carrot);
            Assert.IsNotNull(carrots);
            Assert.AreEqual(1, carrots.Count);
            Assert.AreEqual(ItemType.Carrot, carrots[0].type);



            List<IItem> toRemove = inventory.GetItemsOfType(ItemType.Apple);
            Assert.IsNotNull(toRemove);
            Assert.AreEqual(1, toRemove.Count);
            inventory.RemoveItems(toRemove);
            Assert.AreEqual(2, inventory.ItemList.Count);
        }

    }
}