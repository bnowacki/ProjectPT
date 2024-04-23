namespace CoffeeShop.Data.Catalog
{
    public enum ProductCategory
    {
        Coffee,
        Tea,
        Express,
        Grinder,
        Accessory
    }

    public class Product
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Brand { get; set; } = "";
        public ProductCategory Category { get; set; }

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
