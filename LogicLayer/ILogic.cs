using DataLayer;

namespace LogicLayer
{
    public interface ILogic
    {
        public static ILogic New(IData? data = default)
        {
            return new Logic(data);
        }

        public void CreateOrder(Guid user, Dictionary<Guid, int> products, string? ShippingAddress);
        public void TakeDelivery(Dictionary<Guid, int> products);
    }
}
