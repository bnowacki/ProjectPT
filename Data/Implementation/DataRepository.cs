using Data.API;

namespace Data.Implementation;

internal class DataRepository : IDataRepository
{
    private IDataContext _context;

    public DataRepository(IDataContext context) 
    {
        _context = context;
    }

    #region User CRUD

    public async Task AddUserAsync(int id, string name, string email)
    {
        IUser user = new User(id, name, email);

        await _context.AddUserAsync(user);
    }

    public async Task<IUser> GetUserAsync(int id)
    {
        IUser? user = await _context.GetUserAsync(id);

        if (user is null)
            throw new Exception("This user does not exist!");

        return user;
    }

    public async Task UpdateUserAsync(int id, string name, string email)
    {
        IUser user = new User(id, name, email);

        if (!await CheckIfUserExists(user.Id))
            throw new Exception("This user does not exist");

        await _context.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        if (!await CheckIfUserExists(id))
            throw new Exception("This user does not exist");

        await _context.DeleteUserAsync(id);
    }

    public async Task<Dictionary<int, IUser>> GetAllUsersAsync()
    {
        return await _context.GetAllUsersAsync();
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await _context.GetUsersCountAsync();
    }

    #endregion


    #region Product CRUD

    public async Task AddProductAsync(int id, string name, double price)
    {
        IProduct product = new Game(id, name, price);

        await _context.AddProductAsync(product);
    }

    public async Task<IProduct> GetProductAsync(int id)
    {
        IProduct? product = await _context.GetProductAsync(id);

        if (product is null)
            throw new Exception("This product does not exist!");

        return product;
    }

    public async Task UpdateProductAsync(int id, string name, double price)
    {
        IProduct product = new Game(id, name, price);

        if (!await CheckIfProductExists(product.Id))
            throw new Exception("This product does not exist");

        await _context.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        if (!await CheckIfProductExists(id))
            throw new Exception("This product does not exist");

        await _context.DeleteProductAsync(id);
    }

    public async Task<Dictionary<int, IProduct>> GetAllProductsAsync()
    {
        return await _context.GetAllProductsAsync();
    }

    public async Task<int> GetProductsCountAsync()
    {
        return await _context.GetProductsCountAsync();
    }

    #endregion


    #region State CRUD

    public async Task AddStateAsync(int id, int productId, int productQuantity)
    {
        if (!await _context.CheckIfProductExists(productId))
            throw new Exception("This product does not exist!");

        if (productQuantity < 0)
            throw new Exception("Product's quantity must be number greater that 0!");

        IState state = new State(id, productId, productQuantity);

        await _context.AddStateAsync(state);
    }

    public async Task<IState> GetStateAsync(int id)
    {
        IState? state = await _context.GetStateAsync(id);

        if (state is null)
            throw new Exception("This state does not exist!");

        return state;
    }

    public async Task UpdateStateAsync(int id, int productId, int productQuantity)
    {
        if (!await _context.CheckIfProductExists(productId))
            throw new Exception("This product does not exist!");

        if (productQuantity <= 0)
            throw new Exception("Product's quantity must be number greater that 0!");

        IState state = new State(id, productId, productQuantity);

        if (!await CheckIfStateExists(state.Id))
            throw new Exception("This state does not exist");

        await _context.UpdateStateAsync(state);
    }

    public async Task DeleteStateAsync(int id)
    {
        if (!await CheckIfStateExists(id))
            throw new Exception("This state does not exist");

        await _context.DeleteStateAsync(id);
    }

    public async Task<Dictionary<int, IState>> GetAllStatesAsync()
    {
        return await _context.GetAllStatesAsync();
    }

    public async Task<int> GetStatesCountAsync()
    {
        return await _context.GetStatesCountAsync();
    }

    #endregion


    #region Event CRUD

    public async Task AddEventAsync(int id, int stateId, int userId, string type, int quantity = 0)
    {
        IUser user = await GetUserAsync(userId);
        IState state = await GetStateAsync(stateId);
        IProduct product = await GetProductAsync(state.productId);

        IEvent newEvent = new Event(id, stateId, userId, DateTime.Now, type, quantity);

        switch (type)
        {
            case "PurchaseEvent":
                if (state.productQuantity == 0)
                    throw new Exception("Product unavailable, please check later!");

                await UpdateStateAsync(stateId, product.Id, state.productQuantity - 1);
                await UpdateUserAsync(userId, user.Name, user.Email);

                break;

            case "ReturnEvent":
                Dictionary<int, IEvent> events = await GetAllEventsAsync();
                Dictionary<int, IState> states = await GetAllStatesAsync();

                int copiesBought = 0;

                foreach
                (
                    IEvent even in
                    from even in events.Values
                    from stat in states.Values
                    where even.userId == user.Id &&
                          even.stateId == stat.Id &&
                          stat.productId == product.Id
                    select even
                )
                    if (even.Type == "PurchaseEvent")
                        copiesBought++;
                    else if (even.Type == "ReturnEvent")
                        copiesBought--;

                copiesBought--;

                if (copiesBought < 0)
                    throw new Exception("You do not own this product!");

                await UpdateStateAsync(stateId, product.Id, state.productQuantity + 1);
                await UpdateUserAsync(userId, user.Name, user.Email);

                break;
            case "SupplyEvent":
                if (quantity <= 0)
                    throw new Exception("Can not supply with this amount!");

                await UpdateStateAsync(stateId, product.Id, state.productQuantity + quantity);

                break;

            default:
                throw new Exception("This event type does not exist!");
        }

        await _context.AddEventAsync(newEvent);
    }

    public async Task<IEvent> GetEventAsync(int id)
    {
        IEvent? even = await _context.GetEventAsync(id);

        if (even is null)
            throw new Exception("This event does not exist!");

        return even;
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurenceDate, string type, int? quantity)
    {
        IEvent newEvent = new Event(id, stateId, userId, occurenceDate, type, quantity);

        if (!await CheckIfEventExists(newEvent.Id, type))
            throw new Exception("This event does not exist");

        await _context.UpdateEventAsync(newEvent);
    }

    public async Task DeleteEventAsync(int id)
    {
        if (!await CheckIfEventExists(id, "PurchaseEvent"))
            throw new Exception("This event does not exist");

        await _context.DeleteEventAsync(id);
    }

    public async Task<Dictionary<int, IEvent>> GetAllEventsAsync()
    {
        return await _context.GetAllEventsAsync();
    }

    public async Task<int> GetEventsCountAsync()
    {
        return await _context.GetEventsCountAsync();
    }

    #endregion


    #region Utils

    public async Task<bool> CheckIfUserExists(int id)
    {
        return await _context.CheckIfUserExists(id);
    }

    public async Task<bool> CheckIfProductExists(int id)
    {
        return await _context.CheckIfProductExists(id);
    }

    public async Task<bool> CheckIfStateExists(int id)
    {
        return await _context.CheckIfStateExists(id);
    }

    public async Task<bool> CheckIfEventExists(int id, string type)
    {
        return await _context.CheckIfEventExists(id, type);
    }

    //public async Task<IEvent> GetLastProductEvent(int productId)

    //}

    //public async Task<Dictionary<int, IEvent>> GetProductEventHistory(int productId)
    //{

    //}

    //public async Task<IState> GetProductState(int productId)
    //{

    //}

    #endregion
}
