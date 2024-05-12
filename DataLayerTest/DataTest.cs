using DataLayer;
using DataLayer.Users;
using DataLayer.Catalog;
using DataLayer.State;

namespace DataLayerTest
{
    public abstract class DataTest
    {
        protected IDataContext _context;
        protected IData _data;

        public abstract void Initialize();

        #region users
        [TestMethod]
        public void TestGetUsers()
        {
            List<User> users = _data.GetUsers();
            Assert.AreEqual(_context.Users.Count, users.Count);
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

        #endregion users

        #region products

        [TestMethod]
        public void TestGetProducts()
        {
            List<Product> products = _data.GetProducts();
            Assert.AreEqual(_context.Products.Count, products.Count);
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
                Price = 13.75f
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

        #endregion products

        #region inventory

        [TestMethod]
        public void TestGetStock()
        {
            Dictionary<Guid, int> stock = _data.GetStock();
            Assert.AreSame(_context.Inventory.Stock, stock);
        }

        #endregion inventory

        #region orders

        [TestMethod]
        public void TestGetOrders()
        {
            List<Order> orders = _data.GetOrders();
            Assert.AreEqual(_context.Orders.Count, orders.Count);
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

        #endregion orders
    }
}
