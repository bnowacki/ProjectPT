using CoffeeShop.Data.Catalog;
using CoffeeShop.Data.State;
using CoffeeShop.Data.Users;
using CoffeeShop.Data;

namespace CoffeeShopTest.TestDataGenerators
{
    internal class HardCodedDataGenerator : IDataGenerator
    {
        private List<Product> products;
        private List<User> users;
        private List<Order> orders;
        private Inventory inventory;

        public HardCodedDataGenerator()
        {
            products = new List<Product>
{
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Mocha Madness",
        Description = "Indulge in the madness of our delicious mocha creation.",
        Brand = "Mocha Magic",
        Category = ProductCategory.Coffee,
        Price = 9.75f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Java Jumpstart",
        Description = "Get a jumpstart on your day with the bold flavors of Java.",
        Brand = "Java Junction",
        Category = ProductCategory.Coffee,
        Price = 11.99f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Caffeine Craze",
        Description = "Join the craze with our irresistible caffeine-packed blend.",
        Brand = "Caffeine Co.",
        Category = ProductCategory.Coffee,
        Price = 7.50f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Arabica Awakening",
        Description = "Awaken your senses with the smooth taste of Arabica beans.",
        Brand = "Arabica Aroma",
        Category = ProductCategory.Coffee,
        Price = 10.00f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Brewed Bliss",
        Description = "Savor the bliss of freshly brewed coffee anytime, anywhere.",
        Brand = "BrewMaster",
        Category = ProductCategory.Coffee,
        Price = 8.25f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Bean Bonanza",
        Description = "Experience the bonanza of flavors in every sip.",
        Brand = "Bean Bliss",
        Category = ProductCategory.Coffee,
        Price = 12.99f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Latte Luxe",
        Description = "Luxuriate in the creamy richness of our signature latte.",
        Brand = "LatteLux",
        Category = ProductCategory.Coffee,
        Price = 14.50f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Roast Royale",
        Description = "Embrace the royal taste of our perfectly roasted coffee.",
        Brand = "RoastRoyale",
        Category = ProductCategory.Coffee,
        Price = 13.75f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Tea Time",
        Description = "Relax and enjoy a delightful cup of tea.",
        Brand = "TeaTreat",
        Category = ProductCategory.Tea,
        Price = 6.99f // Hardcoded price
    },
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Espresso Express",
        Description = "Get your caffeine fix in record time with our espresso shot.",
        Brand = "Espresso Express",
        Category = ProductCategory.Express,
        Price = 9.99f // Hardcoded price
    }
};

            inventory = new();
            int i = 1;
            foreach (Product product in products)
            {
                inventory.AddProductToStock(product.Id, i++);
            }

            users = new List<User>
{
    new Employee
    {
        Id = Guid.NewGuid(),
        FirstName = "Alice",
        LastName = "Smith",
        Email = "alice.smith@example.com",
        Phone = "+1122334455",
        Salary = 6000 // Hardcoded salary
    },
    new Customer
    {
        Id = Guid.NewGuid(),
        FirstName = "Bob",
        LastName = "Johnson",
        Email = "bob.johnson@example.com",
        Phone = "+9988776655",
        ShippingAddress = "789 Oak St, Anothertown, USA" // Hardcoded shipping address
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        FirstName = "Charlie",
        LastName = "Williams",
        Email = "charlie.williams@example.com",
        Phone = "+3322114455",
        Salary = 5500 // Hardcoded salary
    },
    new Customer
    {
        Id = Guid.NewGuid(),
        FirstName = "Eva",
        LastName = "Brown",
        Email = "eva.brown@example.com",
        Phone = "+1122338899",
        ShippingAddress = "1011 Pine St, Yetanothertown, USA" // Hardcoded shipping address
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        FirstName = "David",
        LastName = "Miller",
        Email = "david.miller@example.com",
        Phone = "+9988776633",
        Salary = 5800 // Hardcoded salary
    }
};

            orders = new List<Order>{
    new Order
    {
        Id = Guid.NewGuid(),
        User = users[1].Id,
        Products = new Dictionary<Guid, int>
        {
            { products[0].Id, 2 },
            { products[2].Id, 1 },
            { products[3].Id, 3 }
        },
        ShippingCost = 10.5f,
        ShippingAddress = "123 Main St, Anytown, USA",
        Total = 55.75f
    },
    new Order
    {
        Id = Guid.NewGuid(),
        User = users[3].Id,
        Products = new Dictionary<Guid, int>
        {
            { products[3].Id, 1 },
            { products[4].Id, 4 },
            { products[6].Id, 2 }
        },
        ShippingCost = 8.25f,
        ShippingAddress = "456 Elm St, Othertown, USA",
        Total = 72.30f
    },
    new Order
    {
        Id = Guid.NewGuid(),
        User = users[3].Id,
        Products = new Dictionary<Guid, int>
        {
            { products[2].Id, 1 },
            { products[5].Id, 1 },
            { products[7].Id, 1 },
            { products[1].Id, 1 }
        },
        ShippingCost = 5.75f,
        ShippingAddress = "1011 Pine St, Yetanothertown, USA",
        Total = 26.45f
    },
};


        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Inventory GetInventory()
        {
            return inventory;
        }
    }
}
