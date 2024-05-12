using DataLayer;
using DataLayerTest.TestDataGeneration;

namespace DataLayerTest
{
    [TestClass]
    public class RandomDataTest : DataTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            IDataGenerator dataGenerator = new RandomDataGenerator();
            _context = dataGenerator.GetDataContext();
            _data = IData.New(_context);
        }
    }
}
