using DataLayer;
using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayerTest.TestDataGeneration
{
    internal class TestDataContext : IDataContext
    {
        public Dictionary<Guid, Order> Orders { get; set; }
        public Dictionary<Guid, User> Users { get; set; }
        public Dictionary<Guid, Product> Products { get; set; }
        public Inventory Inventory { get; set; }

        public TestDataContext()
        {
            Orders = new Dictionary<Guid, Order>();
            Users = new Dictionary<Guid, User>();
            Products = new Dictionary<Guid, Product>();
            Inventory = new();
        }
    }
}
