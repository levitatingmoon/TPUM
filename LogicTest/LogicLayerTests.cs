using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicLayerTest
    {

        private LogicAbstractApi logicApi = LogicAbstractApi.Create(new DataLayerTest());


        [TestMethod]
        public void GetItemsTest()
        {
            Assert.AreEqual(logicApi.Shop.GetItems().Count,2);
        }


        [TestMethod]
        public void SellItemsTest() {

            ShopItem item = logicApi.Shop.GetItems()[0];
            List<ShopItem> shopItems = new List<ShopItem>();
            shopItems.Add(item);
            int count = logicApi.Shop.GetItems().Count;
            logicApi.Shop.Sell(shopItems);
            Assert.AreEqual(logicApi.Shop.GetItems().Count, 1);

        }
    }
}