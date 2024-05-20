namespace DataLayer.API
{
    public interface IProduct
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
