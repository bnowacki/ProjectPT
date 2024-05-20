using DataLayer.API;

namespace DataLayer.Implementation
{
    internal class Product : IProduct
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        public int Stock { get; set; } = 0;

        private float price;
        public float Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative.");
                }
                price = value;
            }
        }
    }
}
