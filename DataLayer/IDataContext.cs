using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayer
{
    public interface IDataContext
    {
        public Dictionary<Guid, Order> Orders { get; set; }
        public Dictionary<Guid, User> Users { get; set; }
        public Dictionary<Guid, Product> Products { get; set; }
        public Inventory Inventory { get; set; }
    }
}
