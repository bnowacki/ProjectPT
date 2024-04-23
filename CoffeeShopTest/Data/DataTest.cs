using CoffeeShop.Data;
using CoffeeShopTest.TestDataGenerators;
using CoffeeShop.Data.Users;
using CoffeeShop.Data.Catalog;
using CoffeeShop.Data.State;

namespace CoffeeShopTest.Data
{
    [TestClass]
    public class DataTest
    {
        private IData _data;

        [TestInitialize]
        public void Initialize()
        {
            _data = CoffeeShop.Data.Data.New();
            _data.Seed(new RandomDataGenerator());
            //_data.Seed(new HardCodedDataGenerator());
        }

        [TestMethod]
        public void TestUpsertUser()
        {
            Customer c = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bartek",
                LastName = "Kowalski",
                Email = "kowalski@gmail.com",
                Phone = "123456789",
            };
            _data.UpsertUser(c);
            Assert.AreSame(c, _data.GetUser(c.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User not found")]
        public void TestDeleteUser()
        {
            List<User> users = _data.GetUsers();
            if (users.Count == 0)
            {
                throw new Exception("No users");
            }
            User p = users[0];
            Assert.IsTrue(_data.DeleteUser(p.Id));
            _data.GetUser(p.Id);
        }

        [TestMethod]
        public void TestUpsertProduct()
        {
            Product p = new()
            {
                Id = Guid.NewGuid(),
                Name = "Roast Royale",
                Description = "Embrace the royal taste of our perfectly roasted coffee.",
                Brand = "RoastRoyale",
                Category = ProductCategory.Coffee,
                Price = 13.75f // Hardcoded price
            };
            _data.UpsertProduct(p);
            Assert.AreSame(p, _data.GetProduct(p.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Product not found")]
        public void TestDeleteProduct()
        {
            List<Product> products = _data.GetProducts();
            if (products.Count == 0)
            {
                throw new Exception("No products");
            }
            Product p = products[0];
            Assert.IsTrue(_data.DeleteProduct(p.Id));
            _data.GetProduct(p.Id);
        }

        [TestMethod]
        public void TestCreateOrder()
        {
            List<User> users = _data.GetUsers();
            if (users.Count == 0)
            {
                throw new Exception("No users");
            }
            int i = 0;
            User user = users[i];
            while (user is not Customer)
            {
                user = users[++i];
            }

            List<Product> products = _data.GetProducts();
            if (products.Count == 0)
            {
                throw new Exception("No products");
            }
            
            Order o = new()
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
            };

            _data.CreateOrder(o);
            Assert.AreSame(o, _data.GetOrder(o.Id));
        }
    }
}
