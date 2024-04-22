using CoffeShop.Data.Catalog;

namespace CoffeShop.Data.State
{
    public class Order
    {
        public Guid Id { get; init; }
        public Guid User { get; set; }
        public Dictionary<Guid, int> Products { get; set; } = new Dictionary<Guid, int>();
        public float ShippingCost { get; set; }
        public string? ShippingAddress { get; set; }
        public float Total { get; set; }
    }
}
