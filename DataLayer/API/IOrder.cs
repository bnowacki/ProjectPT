namespace DataLayer.API
{
    public interface IOrder
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid User { get; init; }
        public IEnumerable<KeyValuePair<Guid, int>> Products { get; init; }
        public float Total { get; init; }
    }
}
