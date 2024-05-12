using DataLayer;
using DataLayer.Catalog;
using DataLayer.Users;
using DataLayerTest.TestDataGeneration;

namespace DataLayerTest
{
    [TestClass]
    public class HardcodedDataTest : DataTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            IDataGenerator dataGenerator = new HardCodedDataGenerator();
            _context = dataGenerator.GetDataContext();
            _data = IData.New(_context);
        }

        [TestMethod]
        public void TestGetProductStock()
        {
            Guid id = new("C8F39DB2-EBB5-403B-A6C0-C26045A77A29");
            int stock = _data.GetProductStock(id);
            Assert.AreEqual(_context.Inventory.Stock[id], stock);
        }

        [TestMethod]
        public void TestAddExistingProductToStock()
        {
            Guid id = new("ACE4A6C9-A966-43E2-A00C-BF61EFBE7B4A");
            int before = _data.GetProductStock(id);
            int after = _data.AddProductToStock(id, 2);
            Assert.AreEqual(before + 2, after);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Updating would result in negative stock.")]
        public void TestRemoveMoreProductFromStockThanPossible()
        {
            Guid id = new("5F6B70DC-9CD2-4D29-87E6-A0E872FD3755");
            _data.AddProductToStock(id, -100);
        }
    }
}
