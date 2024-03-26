using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {
        private DataAbstractApi dataLayer;
        private IShop shop;


        [TestInitialize]
        public void InitTest()
        {
            StorageMock storage = new StorageMock();

            List<IItem> items = new List<IItem>()
            {
                new Item("Gruszka Lodzka", 14.23f, ItemType.Pear),
                new Item("Marchewka Zielona", 0.90f, ItemType.Carrot)
            };

            storage.AddItems(items);

            dataLayer = new DataLayerTest(storage);
            Assert.IsNotNull(dataLayer);

            shop = new LogicLayer(dataLayer).Shop;
            Assert.IsNotNull(shop);
        }


        [TestMethod]
        public void GetItemsTest()
        {
            List<ShopItem> shopItems = shop.GetItems();
            Assert.IsNotNull(shopItems);
            Assert.AreEqual(2, shopItems.Count);
        }


        [TestMethod]
        public void SetItemsTest() {

            List<ShopItem> shopItems = shop.GetItems();
            Assert.AreEqual(2, shopItems.Count);

            shop.Sell(shopItems);
            shopItems = shop.GetItems();
            Assert.AreEqual(0, shopItems.Count);

        }
    }
}