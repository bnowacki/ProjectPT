using CoffeShop.Data.Catalog;
using CoffeShop.Data.State;
using CoffeShop.Data.Users;

namespace CoffeShop.Data
{
    public interface IDataGenerator
    {
        public List<Order> GetOrders();
        public List<User> GetUsers();
        public List<Product> GetProducts();
        public Inventory GetInventory();
    }
}
