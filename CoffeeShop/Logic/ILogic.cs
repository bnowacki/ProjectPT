using CoffeeShop.Data;

namespace CoffeeShop.Logic
{
    public interface ILogic
    {
        public void CreateOrder(Guid user, Dictionary<Guid, int> products, string? ShippingAddress);
        public void TakeDelivery(Dictionary<Guid, int> products);
    }
}
