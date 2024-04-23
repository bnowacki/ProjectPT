using CoffeeShop.Data.Catalog;
using CoffeeShop.Data.State;
using CoffeeShop.Data.Users;

namespace CoffeeShop.Data
{
    public interface Data
    {
        public static IData New()
        {
            return new DataImplementation();
        }

        private class DataImplementation : IData
        {
            private Dictionary<Guid, Order> orders = new Dictionary<Guid, Order>();
            private Dictionary<Guid, User> users = new Dictionary<Guid, User>();
            private Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
            private Inventory inventory = new();

            public void Seed(IDataGenerator dataGenerator)
            {
                foreach (Product product in dataGenerator.GetProducts())
                {
                    UpsertProduct(product);
                }
                inventory = dataGenerator.GetInventory();
                foreach (User user in dataGenerator.GetUsers())
                {
                    UpsertUser(user);
                }
                foreach (Order order in dataGenerator.GetOrders())
                {
                    CreateOrder(order);
                }
            }

            public int AddProductToStock(Guid id, int n)
            {
                if (!products.ContainsKey(id))
                {
                    throw new ArgumentException("Product with given Id does not exist");
                }
                return inventory.AddProductToStock(id, n);
            }

            public void CreateOrder(Order order)
            {
                if (orders.ContainsKey(order.Id))
                {
                    throw new ArgumentException("Order with given Id already exists");
                }
                if (!users.ContainsKey(order.User))
                {
                    throw new ArgumentException("Customer with given Id does not exist");
                }
                orders.Add(order.Id, order);
            }


            public User GetUser(Guid id)
            {
                if (!users.ContainsKey(id))
                {
                    throw new ArgumentException("User not found");
                }
                return users[id];
            }

            public List<User> GetUsers()
            {
                return new List<User>(users.Values);
            }

            public void UpsertUser(User user)
            {
                users[user.Id] = user;
            }

            public bool DeleteUser(Guid id)
            {
                return users.Remove(id);
            }


            public Order GetOrder(Guid id)
            {
                if (!orders.ContainsKey(id))
                {
                    throw new ArgumentException("Order not found");
                }
                return orders[id];
            }

            public int GetProductStock(Guid id)
            {
                return inventory.GetProductStock(id);
            }

            public Dictionary<Guid, int> GetStock()
            {
                return inventory.Stock;
            }

            public Product GetProduct(Guid id)
            {
                if (!products.ContainsKey(id))
                {
                    throw new ArgumentException("Product not found");
                }
                return products[id];
            }

            public List<Product> GetProducts()
            {
                return new List<Product>(products.Values);
            }

            public void UpsertProduct(Product product)
            {
                products[product.Id] = product;
            }

            public bool DeleteProduct(Guid id)
            {
                return products.Remove(id);
            }
        }
    }
}
