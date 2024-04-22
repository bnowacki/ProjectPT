using CoffeShop.Data.Catalog;
using CoffeShop.Data.Users;
using CoffeShop.Data.State;

namespace CoffeShop.Data
{
    public interface IData
    {
        public void Seed(IDataGenerator dataGenerator);

        // users
        public User GetUser(Guid id);
        public List<User> GetUsers();
        public void UpsertUser(User user);
        public bool DeleteUser(Guid id);

        // products
        public Product GetProduct(Guid id);
        public  List<Product> GetProducts();
        public void UpsertProduct(Product product);
        public bool DeleteProduct(Guid id);

        // inventory
        public Dictionary<Guid, int> GetStock();
        public int GetProductStock(Guid id);
        public int AddProductToStock(Guid id, int n);

        // orders
        public Order GetOrder(Guid id);
        public void CreateOrder(Order order);
    }
}
