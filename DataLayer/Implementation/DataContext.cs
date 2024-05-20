using DataLayer.API;
using DataLayer.Database;

namespace DataLayer.Implementation
{
    internal class DataContext : IDataContext
    {
        private readonly string _connectionString;

        public DataContext(string? connectionString = null)
        {
            if (connectionString is null)
            {
                string _projectRootDir = Environment.CurrentDirectory;
                string _DBRelativePath = @"..\Database\Database.mdf";
                string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
                _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
            }
            else
            {
                _connectionString = connectionString;
            }
        }

        #region Users
        public async Task<IUser> GetUser(Guid id)
        {
            using DatabaseDataContext context = new(_connectionString);

            Database.User? user = await Task.Run(() =>
            {
                IQueryable<Database.User> query =
                    from u in context.Users
                    where u.Id == id
                    select u;

                return query.FirstOrDefault();
            });

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new User { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
        }

        public async Task<IEnumerable<IUser>> GetUsers()
        {
            using DatabaseDataContext context = new(_connectionString);

            IQueryable<IUser> usersQuery = from u in context.Users
                                           select new User
                                           {
                                               Id = u.Id,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                               Email = u.Email
                                           };

            return await Task.Run(() => usersQuery.ToList());
        }

        public async Task<IUser> CreateUser(IUser user)
        {
            using DatabaseDataContext context = new(_connectionString);

            Database.User entity = new Database.User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            context.Users.InsertOnSubmit(entity);

            await Task.Run(() => context.SubmitChanges());

            return await GetUser(user.Id);
        }
        public Task<IUser> UpdateUser(IUser user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Products

        public Task<IProduct> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IProduct>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddProductToStock(Guid id, int n)
        {
            throw new NotImplementedException();
        }

        public Task<IProduct> CreateProduct(IProduct user)
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

        #endregion

        #region Orders

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
