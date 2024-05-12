using DataLayer;

namespace DataLayerTest.TestDataGeneration
{
    public interface IDataGenerator
    {
        public IDataContext GetDataContext();
    }
}
