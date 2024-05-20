using DataLayer.API;

namespace DataLayer.Implementation
{
    internal class Data : IData
    {
        private readonly IDataContext _context;

        public Data(IDataContext? context = default)
        {
            _context = context ?? IDataContext.New();
        }

        #region Users

        Task<IUser> IData.GetUser(Guid id)
        {
            return _context.GetUser(id);
        }

        Task<IEnumerable<IUser>> IData.GetUsers()
        {
            return _context.GetUsers();
        }

        Task<IUser> IData.CreateUser(string firstName, string lastName, string email)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            return _context.CreateUser(user);
        }

        Task<IUser> IData.UpdateUser(Guid id, string firstName, string lastName, string email)
        {
            User user = new()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            return _context.UpdateUser(user);
        }

        Task<bool> IData.DeleteUser(Guid id)
        {
            return _context.DeleteUser(id);
        }

        #endregion

        #region Products
        Task<IProduct> IData.GetProduct(Guid id)
        {
            return _context.GetProduct(id);
        }

        Task<IEnumerable<IProduct>> IData.GetProducts()
        {
            return _context.GetProducts();
        }

        Task<IProduct> IData.CreateProduct(string name, float price, int stock)
        {
            Product product = new() {Id = Guid.NewGuid(), Name = name, Price = price, Stock = stock };

            return _context.CreateProduct(product);
        }

        Task<IProduct> IData.UpdateProduct(Guid id, string name, float price, int stock)
        {
            Product product = new() { Id = id, Name = name, Price = price, Stock = stock };

            return _context.UpdateProduct(product);
        }

        async Task IData.UpdateProductStock(Guid id, int stock)
        {
            IProduct product = await _context.GetProduct(id);
            product.Stock = stock;

            await _context.UpdateProduct(product);

            return;
        }


        Task IData.DeleteProduct(Guid id)
        {
            return _context.DeleteProduct(id);
        }

        #endregion

        #region Orders
        Task<IOrder> IData.GetOrder(Guid id)
        {
            return _context.GetOrder(id);
        }

        Task<IEnumerable<IOrder>> IData.GetOrders()
        {
            return _context.GetOrders();
        }

        Task<IOrder> IData.CreateOrder(Guid userId, Dictionary<Guid, int> products, float total)
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Products = products,
                Total = total,
                User = userId,
            };

            return _context.CreateOrder(order);
        }

        #endregion Orders
    }
}
