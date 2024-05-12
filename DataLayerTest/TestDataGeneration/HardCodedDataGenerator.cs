using DataLayer;
using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayerTest.TestDataGeneration
{
    internal class HardCodedDataGenerator : IDataGenerator
    {
        private IDataContext _context;

        public HardCodedDataGenerator()
        {
            _context = new TestDataContext();

            GenerateProducts();
            GenerateInventory();
            GenerateUsers();
            GenerateOrders();
        }

        public IDataContext GetDataContext()
        {
            return _context;
        }

        private void GenerateProducts()
        {
            List<Product> products = new()
            {
                new Product
                {
                    Id = new Guid("3869AC88-6D6B-46CD-8208-61F82002D49A"),
                    Name = "Mocha Madness",
                    Description = "Indulge in the madness of our delicious mocha creation.",
                    Brand = "Mocha Magic",
                    Category = ProductCategory.Coffee,
                    Price = 9.75f
                },
                new Product
                {
                    Id = new Guid("A2372FE4-F5AB-40D4-8078-4BF82E9D8D1B"),
                    Name = "Java Jumpstart",
                    Description = "Get a jumpstart on your day with the bold flavors of Java.",
                    Brand = "Java Junction",
                    Category = ProductCategory.Coffee,
                    Price = 11.99f
                },
                new Product
                {
                    Id = new Guid("CCA14795-9BE2-4042-862B-E59C13F7A064"),
                    Name = "Caffeine Craze",
                    Description = "Join the craze with our irresistible caffeine-packed blend.",
                    Brand = "Caffeine Co.",
                    Category = ProductCategory.Coffee,
                    Price = 7.50f
                },
                new Product
                {
                    Id = new Guid("C6627A79-730D-4706-8A66-0D8C140595FA"),
                    Name = "Arabica Awakening",
                    Description = "Awaken your senses with the smooth taste of Arabica beans.",
                    Brand = "Arabica Aroma",
                    Category = ProductCategory.Coffee,
                    Price = 10.00f
                },
                new Product
                {
                    Id = new Guid("C2D2468B-B4B7-4D3A-A95E-351C7F171EF4"),
                    Name = "Brewed Bliss",
                    Description = "Savor the bliss of freshly brewed coffee anytime, anywhere.",
                    Brand = "BrewMaster",
                    Category = ProductCategory.Coffee,
                    Price = 8.25f
                },
                new Product
                {
                    Id = new Guid("6ABA2982-CA2C-4EBC-B4E6-EE1ED90919B6"),
                    Name = "Bean Bonanza",
                    Description = "Experience the bonanza of flavors in every sip.",
                    Brand = "Bean Bliss",
                    Category = ProductCategory.Coffee,
                    Price = 12.99f
                },
                new Product
                {
                    Id = new Guid("9338BC8A-A12B-4BD8-995B-A16C0370796F"),
                    Name = "Latte Luxe",
                    Description = "Luxuriate in the creamy richness of our signature latte.",
                    Brand = "LatteLux",
                    Category = ProductCategory.Coffee,
                    Price = 14.50f
                },
                new Product
                {
                    Id = new Guid("C8F39DB2-EBB5-403B-A6C0-C26045A77A29"),
                    Name = "Roast Royale",
                    Description = "Embrace the royal taste of our perfectly roasted coffee.",
                    Brand = "RoastRoyale",
                    Category = ProductCategory.Coffee,
                    Price = 13.75f
                },
                new Product
                {
                    Id = new Guid("ACE4A6C9-A966-43E2-A00C-BF61EFBE7B4A"),
                    Name = "Tea Time",
                    Description = "Relax and enjoy a delightful cup of tea.",
                    Brand = "TeaTreat",
                    Category = ProductCategory.Tea,
                    Price = 6.99f
                },
                new Product
                {
                    Id = new Guid("5F6B70DC-9CD2-4D29-87E6-A0E872FD3755"),
                    Name = "Espresso Express",
                    Description = "Get your caffeine fix in record time with our espresso shot.",
                    Brand = "Espresso Express",
                    Category = ProductCategory.Express,
                    Price = 9.99f
                }
            };

            foreach (Product product in products)
            {
                _context.Products.Add(product.Id, product);
            }
        }

        private void GenerateInventory()
        {
            int i = 1;
            foreach (Product product in new List<Product>(_context.Products.Values))
            {
                _context.Inventory.AddProductToStock(product.Id, i++);
            }
        }

        private void GenerateUsers()
        {
            List<User> users = new()
            {
                new Employee
                {
                    Id = new Guid("6258779F-B17E-4671-A6C2-CDE9290410F3"),
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice.smith@example.com",
                    Phone = "+1122334455",
                    Salary = 6000 // Hardcoded salary
                },
                new Customer
                {
                    Id = new Guid("17A85441-ABA7-4AEA-B83D-0D61A9977218"),
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@example.com",
                    Phone = "+9988776655",
                    ShippingAddress = "789 Oak St, Anothertown, USA" // Hardcoded shipping address
                },
                new Employee
                {
                    Id = new Guid("8231BC57-C367-4D61-99F2-DFB01552CBD9"),
                    FirstName = "Charlie",
                    LastName = "Williams",
                    Email = "charlie.williams@example.com",
                    Phone = "+3322114455",
                    Salary = 5500 // Hardcoded salary
                },
                new Customer
                {
                    Id = new Guid("76F05F5C-D55D-4032-AD72-0B32A81ED31B"),
                    FirstName = "Eva",
                    LastName = "Brown",
                    Email = "eva.brown@example.com",
                    Phone = "+1122338899",
                    ShippingAddress = "1011 Pine St, Yetanothertown, USA" // Hardcoded shipping address
                },
                new Employee
                {
                    Id = new Guid("CAA8CFFA-B465-405C-9D2D-8737F26B1FA0"),
                    FirstName = "David",
                    LastName = "Miller",
                    Email = "david.miller@example.com",
                    Phone = "+9988776633",
                    Salary = 5800 // Hardcoded salary
                }
            };

            foreach (User user in users)
            {
                _context.Users.Add(user.Id, user);
            }
        }

        private void GenerateOrders()
        {
            List<Product> products = new(_context.Products.Values);
            List<User> users = new(_context.Users.Values);

            List<Order> orders = new()
            {
                new Order
                {
                    Id = new Guid("7E8C6887-5719-493E-94F4-27D9C859B60C"),
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
                    Id = new Guid("8EA0E803-9426-48EB-8315-1025644542B7"),
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
                    Id = new Guid("F1E52C96-F419-4596-8264-D0874B0B7EDA"),
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

            foreach (Order order in orders)
            {
                _context.Orders.Add(order.Id, order);
            }
        }
    }
}
