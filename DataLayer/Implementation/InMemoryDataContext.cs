using DataLayer.API;

namespace DataLayer.Implementation
{
    internal class InMemoryDataContext : IDataContext
    {
        public InMemoryDataContext()
        {
            Orders = new Dictionary<Guid, IOrder>();
            Users = new Dictionary<Guid, IUser>();
            Products = new Dictionary<Guid, IProduct>();
        }

        #region Users
        public Dictionary<Guid, IUser> Users { get; set; }

        public Task<IUser> GetUser(Guid id)
        {
            if (Users.TryGetValue(id, out IUser? user))
            {
                return Task.FromResult(user);
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }

        public Task<IEnumerable<IUser>> GetUsers()
        {
            return Task.FromResult<IEnumerable<IUser>>(Users.Values);
        }
        public Task<IUser> CreateUser(IUser user)
        {
            if (Users.ContainsKey(user.Id))
            {
                throw new InvalidOperationException("User not found");
            }

            Users.Add(user.Id, user);
            return GetUser(user.Id);
        }

        public Task<IUser> UpdateUser(IUser user)
        {
            if (!Users.ContainsKey(user.Id))
            {
                throw new InvalidOperationException($"A user with ID {user.Id} does not exists.");
            }

            Users[user.Id] = user;
            return GetUser(user.Id);
        }

        public Task<bool> DeleteUser(Guid id)
        {
            return Task.FromResult(Users.Remove(id));
        }

        #endregion Users

        #region Products

        public Dictionary<Guid, IProduct> Products { get; set; }

        public Task<IProduct> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IProduct>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<IProduct> CreateProduct(IProduct product)
        {
            throw new NotImplementedException();
        }

        public Task<IProduct> UpdateProduct(IProduct product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddProductToStock(Guid id, int n)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Orders

        public Dictionary<Guid, IOrder> Orders { get; set; }

        public Task<IOrder> GetOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IOrder>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<IOrder> CreateOrder(IOrder order)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
