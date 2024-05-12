using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayer
{
    internal class Data : IData
    {
        private IDataContext _context;

        public Data()
        {
            _context = new InMemoryDataContext();
        }

        public Data(IDataContext context)
        {
            _context = context;
        }

        #region users
        public User GetUser(Guid id)
        {
            if (!_context.Users.ContainsKey(id))
            {
                throw new ArgumentException("User not found");
            }
            return _context.Users[id];
        }

        public List<User> GetUsers()
        {
            return new List<User>(_context.Users.Values);
        }

        public void UpsertUser(User user)
        {
            _context.Users[user.Id] = user;
        }

        public bool DeleteUser(Guid id)
        {
            return _context.Users.Remove(id);
        }

        #endregion users

        #region products

        public Product GetProduct(Guid id)
        {
            if (!_context.Products.ContainsKey(id))
            {
                throw new ArgumentException("Product not found");
            }
            return _context.Products[id];
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(_context.Products.Values);
        }

        public void UpsertProduct(Product product)
        {
            _context.Products[product.Id] = product;
        }

        public bool DeleteProduct(Guid id)
        {
            return _context.Products.Remove(id);
        }

        #endregion products

        #region inventory

        public int AddProductToStock(Guid id, int n)
        {
            if (!_context.Products.ContainsKey(id))
            {
                throw new ArgumentException("Product with given Id does not exist");
            }
            return _context.Inventory.AddProductToStock(id, n);
        }

        public int GetProductStock(Guid id)
        {
            return _context.Inventory.GetProductStock(id);
        }

        public Dictionary<Guid, int> GetStock()
        {
            return _context.Inventory.Stock;
        }

        #endregion inventory

        #region orders

        public void CreateOrder(Order order)
        {
            if (_context.Orders.ContainsKey(order.Id))
            {
                throw new ArgumentException("Order with given Id already exists");
            }
            if (!_context.Users.ContainsKey(order.User))
            {
                throw new ArgumentException("Customer with given Id does not exist");
            }
            _context.Orders.Add(order.Id, order);
        }

        public Order GetOrder(Guid id)
        {
            if (!_context.Orders.ContainsKey(id))
            {
                throw new ArgumentException("Order not found");
            }
            return _context.Orders[id];
        }

        public List<Order> GetOrders()
        {
            return new List<Order>(_context.Orders.Values);
        }

        #endregion orders
    }
}
