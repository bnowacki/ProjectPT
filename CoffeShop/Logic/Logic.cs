using CoffeShop.Data;
using CoffeShop.Data.Catalog;
using CoffeShop.Data.State;
using CoffeShop.Data.Users;


namespace CoffeShop.Logic
{
    public interface Logic
    {
        public static ILogic New(IData data)
        {
            return new LogicImplementation(data);
        }

        private class LogicImplementation : ILogic
        {
            private IData data = Data.Data.New();

            public LogicImplementation() { }

            public LogicImplementation(IData data)
            {
                this.data = data;
            }

            public void CreateOrder(Guid customerId, Dictionary<Guid, int> products, string? ShippingAddress)
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

                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    User = customerId,
                    Products = products,
                    ShippingAddress = ShippingAddress ?? customer.ShippingAddress, // use passed shipping address or customers default address
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
}
