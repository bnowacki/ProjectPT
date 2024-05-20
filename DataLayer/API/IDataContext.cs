using DataLayer.Implementation;

namespace DataLayer.API
{
    public interface IDataContext
    {
        static IDataContext New(string? connectionString = null)
        {
            return new DataContext(connectionString);
        }

        #region Users

        Task<IUser> GetUser(Guid id);
        Task<IEnumerable<IUser>> GetUsers();
        Task<IUser> CreateUser(IUser user);
        Task<IUser> UpdateUser(IUser user);
        Task<bool> DeleteUser(Guid id);

        #endregion User

        #region Products

        Task<IProduct> GetProduct(Guid id);
        Task<IEnumerable<IProduct>> GetProducts();
        Task<IProduct> CreateProduct(IProduct user);
        Task<IProduct> UpdateProduct(IProduct product);
        Task DeleteProduct(Guid id);
        Task<int> AddProductToStock(Guid id, int n);

        #endregion

        #region Orders

        Task<IOrder> GetOrder(Guid id);
        Task<IEnumerable<IOrder>> GetOrders();
        Task<IOrder> CreateOrder(IOrder order);

        #endregion
    }
}
