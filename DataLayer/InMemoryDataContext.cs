using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayer
{
    internal class InMemoryDataContext : IDataContext
    {
        public Dictionary<Guid, Order> Orders { get; set; }
        public Dictionary<Guid, User> Users { get; set; }
        public Dictionary<Guid, Product> Products { get; set; }
        public Inventory Inventory { get; set; }

        public InMemoryDataContext()
        {
            Orders = new Dictionary<Guid, Order>();
            Users = new Dictionary<Guid, User>();
            Products = new Dictionary<Guid, Product>();
            Inventory = new();
        }
    }
}
