using CoffeeShop.Data;
using CoffeeShop.Data.Catalog;
using CoffeeShop.Data.State;
using CoffeeShop.Data.Users;

namespace CoffeeShopTest.TestDataGenerators
{
    internal class RandomDataGenerator : IDataGenerator
    {
        private List<Product> products;
        private List<User> users;
        private readonly Random random = new(DateTime.Now.Millisecond);

        public RandomDataGenerator()
        {
            products = GenerateProducts();
            users = GenerateUsers();
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        private List<Product> GenerateProducts()
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

            List<Product> products = new();
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
                products.Add(product);
            }

            return products;
        }

        public Inventory GetInventory()
        {
            Inventory inv = new();
            foreach (Product product in products)
            {
                inv.AddProductToStock(product.Id, random.Next(10, 201));
            }
            return inv;
        }

        public List<User> GetUsers()
        {
            return users;
        }

        private List<User> GenerateUsers()
        {
            string[] firstNames = { "John", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Ava", "Alexander", "Isabella" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
            string[] domains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "icloud.com" };
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };

            List<User> users = new();
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
                users.Add(employee);

                Customer customer = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Email = GenerateRandomEmail(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], domains[random.Next(domains.Length)]),
                    Phone = GenerateRandomPhoneNumber(),
                    ShippingAddress = cities[random.Next(cities.Length)],
                };
                users.Add(customer);
            }

            return users;
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

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
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
                orders.Add(order);
            }
            return orders;
        }
    }
}
