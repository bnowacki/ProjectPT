using DataLayer.API;
using LogicLayer.Implementation;

namespace LogicLayer.API
{
    public interface ILogic
    {
        public static ILogic New(IData? data = default)
        {
            return new Logic(data);
        }

        public Task<IOrder> CreateOrder(Guid user, Dictionary<Guid, int> products);
        public Task TakeDelivery(Dictionary<Guid, int> products);

        #region Users

        Task<IUserDTO> GetUser(Guid id);
        Task<IEnumerable<IUserDTO>> GetUsers();
        Task<IUserDTO> CreateUser(string firstName, string lastName, string email);
        Task<IUserDTO> UpdateUser(Guid id, string firstName, string lastName, string email);
        Task<bool> DeleteUser(Guid id);

        #endregion User
    }
}
