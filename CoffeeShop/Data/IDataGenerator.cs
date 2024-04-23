using CoffeeShop.Data.Catalog;
using CoffeeShop.Data.State;
using CoffeeShop.Data.Users;

namespace CoffeeShop.Data
{
    public interface IDataGenerator
    {
        public List<Order> GetOrders();
        public List<User> GetUsers();
        public List<Product> GetProducts();
        public Inventory GetInventory();
    }
}
