using DataLayer.API;

namespace DataLayer.Implementation
{
    internal class Order : IOrder
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid User { get; init; }
        public IEnumerable<KeyValuePair<Guid, int>> Products { get; init; } = new Dictionary<Guid, int>();
        public float Total { get; init; }
    }
}
