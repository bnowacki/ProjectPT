using DataLayer.Implementation;
using System.Security.Cryptography.X509Certificates;

namespace DataLayer.API
{
    public interface IData
    {
        public static IData New(IDataContext? context = default)
        {
            return new Data(context ?? new DataContext());
        }

        #region Users

        Task<IUser> GetUser(Guid id);
        Task<IEnumerable<IUser>> GetUsers();
        Task<IUser> CreateUser(string firstName, string lastName, string email);
        Task<IUser> UpdateUser(Guid id, string firstName, string lastName, string email);
        Task<bool> DeleteUser(Guid id);

        #endregion User

        #region Products

        Task<IProduct> GetProduct(Guid id);
        Task<IEnumerable<IProduct>> GetProducts();
        Task<IProduct> CreateProduct(string name, float price, int stock);
        Task<IProduct> UpdateProduct(Guid id, string name, float price, int stock);
        Task UpdateProductStock(Guid id, int stock);

        Task DeleteProduct(Guid id);

        #endregion

        #region Orders

        Task<IOrder> GetOrder(Guid id);
        Task<IEnumerable<IOrder>> GetOrders();
        Task<IOrder> CreateOrder(Guid userId, Dictionary<Guid, int> products, float total);

        #endregion
    }
}
