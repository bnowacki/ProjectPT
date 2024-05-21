using Data.API;
using Data.Database;

namespace Data.Implementation;

internal class DataContext : IDataContext
{
    public DataContext(string? connectionString = null)
    {
        if (connectionString is null)
        {
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string _DBRelativePath = @"Data\Database\Database.mdf";
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }
        else
        {
            _connectionString = connectionString;
        }
    }

    private readonly string _connectionString;

    #region User CRUD

    public async Task AddUserAsync(IUser user)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.User entity = new Database.User()
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
            };

            context.Users.InsertOnSubmit(entity);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<IUser?> GetUserAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.User? user = await Task.Run(() =>
            {
                IQueryable<Database.User> query = context.Users.Where(u => u.id == id);

                return query.FirstOrDefault();
            });
        
            return user is not null ? new User(user.id, user.name, user.email) : null;
        }
    }

    public async Task UpdateUserAsync(IUser user)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.User toUpdate = (from u in context.Users where u.id == user.Id select u).FirstOrDefault()!;

            toUpdate.name = user.Name;
            toUpdate.email = user.Email;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.User toDelete = (from u in context.Users where u.id == id select u).FirstOrDefault()!;

            context.Users.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<int, IUser>> GetAllUsersAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IUser> usersQuery = from u in context.Users
                select
                    new User(u.id, u.name, u.email) as IUser;

            return await Task.Run(() => usersQuery.ToDictionary(k => k.Id));
        }
    }

    public async Task<int> GetUsersCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Users.Count());
        }
    }

    #endregion


    #region Product CRUD

    public async Task AddProductAsync(IProduct product)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product entity = new Database.Product()
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
            };

            context.Products.InsertOnSubmit(entity);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<IProduct?> GetProductAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product? product = await Task.Run(() =>
            {
                IQueryable<Database.Product> query =
                    from p in context.Products
                    where p.id == id
                    select p;

                return query.FirstOrDefault();
            });

            return product is not null ? new Game(product.id, product.name, (double)product.price) : null;
        }
    }

    public async Task UpdateProductAsync(IProduct product)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product toUpdate = (from p in context.Products where p.id == product.Id select p).FirstOrDefault()!;

            toUpdate.name = product.Name;
            toUpdate.price = product.Price;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product toDelete = (from p in context.Products where p.id == id select p).FirstOrDefault()!;

            context.Products.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<int, IProduct>> GetAllProductsAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IProduct> productQuery = from p in context.Products
                select
                    new Game(p.id, p.name, (double)p.price) as IProduct;

            return await Task.Run(() => productQuery.ToDictionary(k => k.Id));
        }
    }

    public async Task<int> GetProductsCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Products.Count());
        }
    }

    #endregion


    #region State CRUD

    public async Task AddStateAsync(IState state)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.State entity = new Database.State()
            {
                id = state.Id,
                productId = state.productId,
                productQuantity = state.productQuantity
            };

            context.States.InsertOnSubmit(entity);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<IState?> GetStateAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.State? state = await Task.Run(() =>
            {
                IQueryable<Database.State> query =
                    from s in context.States
                    where s.id == id
                    select s;

                return query.FirstOrDefault();
            });

            return state is not null ? new State(state.id, state.productId, state.productQuantity) : null;
        }
    }

    public async Task UpdateStateAsync(IState state)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.State toUpdate = (from s in context.States where s.id == state.Id select s).FirstOrDefault()!;

            toUpdate.productId = state.productId;
            toUpdate.productQuantity = state.productQuantity;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteStateAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.State toDelete = (from s in context.States where s.id == id select s).FirstOrDefault()!;

            context.States.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<int, IState>> GetAllStatesAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IState> stateQuery = from s in context.States
                select
                    new State(s.id, s.productId, s.productQuantity) as IState;

            return await Task.Run(() => stateQuery.ToDictionary(k => k.Id));
        }
    }

    public async Task<int> GetStatesCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.States.Count());
        }
    }

    #endregion


    #region Event CRUD

    public async Task AddEventAsync(IEvent even)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event entity = new Database.Event()
            {
                id = even.Id,
                stateId = even.stateId,
                userId = even.userId,
                occurrenceDate = even.occurrenceDate,
                type = even.Type,
                quantity = even.Quantity
            };

            context.Events.InsertOnSubmit(entity);

            await Task.Run(() => context.SubmitChanges());
        }
    }    

    public async Task<IEvent?> GetEventAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event? even = await Task.Run(() =>
            {
                IQueryable<Database.Event> query =
                    from e in context.Events
                    where e.id == id
                    select e;

                return query.FirstOrDefault();
            });

            return even is not null ? new Event(even.id, even.stateId, even.userId, even.occurrenceDate, even.type, even.quantity) : null;
        }
        
    }    

    public async Task UpdateEventAsync(IEvent even)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event toUpdate = (from e in context.Events where e.id == even.Id select e).FirstOrDefault()!;

            toUpdate.stateId = even.stateId;
            toUpdate.userId = even.userId;
            toUpdate.occurrenceDate = even.occurrenceDate;
            toUpdate.type = even.Type;
            toUpdate.quantity = even.Quantity;

            await Task.Run(() => context.SubmitChanges());
        }
    }    

    public async Task DeleteEventAsync(int id)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event toDelete = (from e in context.Events where e.id == id select e).FirstOrDefault()!;

            context.Events.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }    

    public async Task<Dictionary<int, IEvent>> GetAllEventsAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IEvent> eventQuery = from e in context.Events
                select
                    new Event(e.id, e.stateId, e.userId, e.occurrenceDate, e.type, e.quantity) as IEvent;

            return await Task.Run(() => eventQuery.ToDictionary(k => k.Id));
        }
    }    

    public async Task<int> GetEventsCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Events.Count());
        }
    }    

    #endregion


    #region Utils

    public async Task<bool> CheckIfUserExists(int id)
    {
        return (await GetUserAsync(id)) != null;
    }

    public async Task<bool> CheckIfProductExists(int id)
    {
        return (await GetProductAsync(id)) != null;
    }

    public async Task<bool> CheckIfStateExists(int id)
    {
        return (await GetStateAsync(id)) != null;
    }

    public async Task<bool> CheckIfEventExists(int id, string type)
    {
        return (await GetEventAsync(id)) != null;
    }

    #endregion
}

