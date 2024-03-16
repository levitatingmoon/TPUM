namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSubtract()
        {
            Model.Calculator x = new Model.Calculator();
            int y = x.Subtract(2, 1);
            Assert.AreEqual(1, y);
        }
    }
}