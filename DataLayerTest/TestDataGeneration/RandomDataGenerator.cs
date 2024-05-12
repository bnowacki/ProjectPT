using DataLayer;
using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;

namespace DataLayerTest.TestDataGeneration
{
    internal class RandomDataGenerator : IDataGenerator
    {
        private readonly Random random = new(DateTime.Now.Millisecond);

        private IDataContext _context;

        public RandomDataGenerator()
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
            string[] names = {
                "Morning Brew Bliss",
                "Espresso Euphoria",
                "Mocha Madness",
                "Java Jumpstart",
                "Caffeine Craze",
                "Arabica Awakening",
                "Brewed Bliss",
                "Bean Bonanza",
                "Latte Luxe",
                "Roast Royale"
            };
            string[] descriptions = {
                "Start your day with a blissful cup of morning brew.",
                "Experience pure euphoria with our rich espresso blend.",
                "Indulge in the madness of our delicious mocha creation.",
                "Get a jumpstart on your day with the bold flavors of Java.",
                "Join the craze with our irresistible caffeine-packed blend.",
                "Awaken your senses with the smooth taste of Arabica beans.",
                "Savor the bliss of freshly brewed coffee anytime, anywhere.",
                "Experience the bonanza of flavors in every sip.",
                "Luxuriate in the creamy richness of our signature latte.",
                "Embrace the royal taste of our perfectly roasted coffee."
            };
            string[] brands = {
                "BeanSip",
                "Espresso Elite",
                "Mocha Magic",
                "Java Junction",
                "Caffeine Co.",
                "Arabica Aroma",
                "BrewMaster",
                "Bean Bliss",
                "LatteLux",
                "RoastRoyale"
            };
            ProductCategory[] categories = {
                ProductCategory.Coffee,
                ProductCategory.Tea,
                ProductCategory.Express,
                ProductCategory.Grinder,
                ProductCategory.Accessory
            };

            for (int i = 0; i < 20; i++)
            {
                Product product = new()
                {
                    Id = Guid.NewGuid(),
                    Name = names[random.Next(names.Length)],
                    Description = descriptions[random.Next(descriptions.Length)],
                    Brand = brands[random.Next(brands.Length)],
                    Category = categories[random.Next(categories.Length)],
                    Price = (float)Math.Round(random.NextDouble() * (500 - 5) + 5, 2) // Random price between 5 and 500
                };
                _context.Products.Add(product.Id, product);
            }
        }

        public void GenerateInventory()
        {
            foreach (Product product in new List<Product>(_context.Products.Values))
            {
                _context.Inventory.AddProductToStock(product.Id, random.Next(10, 201));
            }
        }

        private void GenerateUsers()
        {
            string[] firstNames = { "John", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Ava", "Alexander", "Isabella" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
            string[] domains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "icloud.com" };
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };

            for (int i = 0; i < 20; i++)
            {
                Employee employee = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Email = GenerateRandomEmail(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], domains[random.Next(domains.Length)]),
                    Phone = GenerateRandomPhoneNumber(),
                    Salary = random.Next(3000, 10000),
                };
                _context.Users.Add(employee.Id, employee);

                Customer customer = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Email = GenerateRandomEmail(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], domains[random.Next(domains.Length)]),
                    Phone = GenerateRandomPhoneNumber(),
                    ShippingAddress = cities[random.Next(cities.Length)],
                };
                _context.Users.Add(customer.Id, customer);
            }
        }

        private static string GenerateRandomEmail(string firstName, string lastName, string domain)
        {
            return $"{firstName.ToLower()}.{lastName.ToLower()}@{domain}";
        }

        private static string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            string phoneNumber = "+";
            for (int i = 0; i < 10; i++)
            {
                phoneNumber += random.Next(0, 10).ToString();
            }
            return phoneNumber;
        }

        public void GenerateOrders()
        {
            List<Product> products = new(_context.Products.Values);
            List<User> users = new(_context.Users.Values);

            for (int i = 0; i < 50; i++)
            {
                Dictionary<Guid, int> orderProducts = new();
                float total = 0;
                int numProducts = random.Next(1, 6); // Generate random number of products in the order (1 to 5)
                for (int j = 0; j < numProducts; j++)
                {
                    Product product = products[random.Next(products.Count)];
                    while (orderProducts.ContainsKey(product.Id))
                    {
                        product = products[random.Next(products.Count)];
                    }
                    int quantity = random.Next(1, 6); // Random quantity between 1 and 5
                    orderProducts.Add(product.Id, quantity);
                    total += product.Price;
                }

                User user = users[random.Next(users.Count)];
                while (user is not Customer)
                {
                    user = users[random.Next(users.Count)];
                }

                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    User = user.Id,
                    Products = orderProducts,
                    ShippingCost = (float)Math.Round(random.NextDouble() * (50 - 5) + 5, 2), // Random shipping cost between 5 and 50
                    ShippingAddress = ((Customer)user).ShippingAddress,
                    Total = total
                };
                _context.Orders.Add(order.Id, order);
            }
        }
    }
}
