using DataLayer;
using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;


namespace LogicLayer
{
    internal class Logic : ILogic
    {
        private IData data;

        public Logic(IData? data = default)
        {
            this.data = data ?? IData.New();
        }

        public void CreateOrder(Guid customerId, Dictionary<Guid, int> products, string? _shippingAddress)
        {
            float total = 0;
            foreach (KeyValuePair<Guid, int> kvp in products)
            {
                Product product = data.GetProduct(kvp.Key);
                data.AddProductToStock(kvp.Key, kvp.Value * -1);

                total += product.Price * kvp.Value;
            }

            User user = data.GetUser(customerId);
            if (user is not Customer)
            {
                throw new ArgumentException("User with given id is not a customer");
            }
            Customer customer = (Customer)user;

            // use passed shipping address or customers default address
            string? shippingAddress = _shippingAddress ?? customer.ShippingAddress;
            if (shippingAddress == null)
            {
                throw new ArgumentException("no shipping address");
            }

            Order order = new()
            {
                Id = Guid.NewGuid(),
                User = customerId,
                Products = products,
                ShippingAddress = shippingAddress,
                ShippingCost = 12.0f, // TODO: calculate ShippingCost
                Total = total
            };
            data.CreateOrder(order);
        }

        public void TakeDelivery(Dictionary<Guid, int> products)
        {
            foreach (KeyValuePair<Guid, int> kvp in products)
            {
                data.AddProductToStock(kvp.Key, kvp.Value);
            }
        }
    }
}
