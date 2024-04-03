using LogicServer;

namespace LogicServerTest
{
    [TestClass]
    public class LogicServerLayerTest
    {

        private LogicAbstractApi logicApi = LogicAbstractApi.Create(new DataServerLayerTest());


        [TestMethod]
        public void GetItemsTest()
        {
            Assert.AreEqual(logicApi.Shop.GetItems().Count, 2);
        }


        [TestMethod]
        public void SellItemsTest()
        {

            IShopItem item = logicApi.Shop.GetItems()[0];
            List<IShopItem> shopItems = new List<IShopItem>();
            shopItems.Add(item);
            int count = logicApi.Shop.GetItems().Count;
            logicApi.Shop.Sell(shopItems);
            Assert.AreEqual(logicApi.Shop.GetItems().Count, 1);

        }
    }
}