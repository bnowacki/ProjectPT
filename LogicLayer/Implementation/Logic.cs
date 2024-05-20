using DataLayer.API;
using LogicLayer.API;
using System.Collections;

namespace LogicLayer.Implementation
{
    internal class Logic : ILogic
    {
        private IData _data;

        public Logic(IData? data = default)
        {
            _data = data ?? IData.New();
        }

        #region Users

        private static IUserDTO Map(IUser user)
        {
            return new UserDTO { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email};
        }

        public async Task<IUserDTO> GetUser(Guid id)
        {
            return Map(await _data.GetUser(id));
        }

        public async Task<IEnumerable<IUserDTO>> GetUsers()
        {
            List<IUserDTO> result = new();

            foreach (IUser user in (await _data.GetUsers()))
            {
                result.Add(Map(user));
            }

            return result;
        }

        public async Task<IUserDTO> CreateUser(string firstName, string lastName, string email)
        {
            return Map(await _data.CreateUser(firstName, lastName, email));
        }

        public async Task<IUserDTO> UpdateUser(Guid id, string firstName, string lastName, string email)
        {
            return Map(await _data.UpdateUser(id, firstName, lastName, email));
        }

        public Task<bool> DeleteUser(Guid id)
        {
            return _data.DeleteUser(id);
        }

        #endregion

        public async Task<IOrder> CreateOrder(Guid customerId, Dictionary<Guid, int> products)
        {
            float total = 0;
            foreach (KeyValuePair<Guid, int> kvp in products)
            {
                IProduct product = await _data.GetProduct(kvp.Key);
                await AddProductToStock(kvp.Key, kvp.Value * -1);

                total += product.Price * kvp.Value;
            }

            return await _data.CreateOrder(customerId, products, total);
        }

        public async Task TakeDelivery(Dictionary<Guid, int> products)
        {
            foreach (KeyValuePair<Guid, int> kvp in products)
            {
                await AddProductToStock(kvp.Key, kvp.Value);
            }
        }

        async Task<int> AddProductToStock(Guid id, int n)
        {

            IProduct product = await _data.GetProduct(id);

            int newStock = product.Stock + n;
            if (newStock < 0)
            {
                throw new ArgumentException("Updating would result in negative stock.");
            }

            await _data.UpdateProductStock(id, newStock);

            return newStock;
        }
    }
}
