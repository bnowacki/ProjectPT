using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayer
{
    public interface IData
    {
        public static IData New(IDataContext? context = default)
        {
            return new Data(context ?? new InMemoryDataContext());
        }

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
        public List<Order> GetOrders();
        public void CreateOrder(Order order);
    }
}
