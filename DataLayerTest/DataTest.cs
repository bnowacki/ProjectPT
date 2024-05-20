using DataLayer.API;
using DataLayer.Database;

namespace DataLayerTest
{
    [TestClass]
    [DeploymentItem(@"Instrumentation\TestDatabase.mdf", @"Instrumentation")]
    public class DataTest
    {
        private static IData _data;
        private static string _connectionString;

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _DBRelativePath = @"Instrumentation\TestDatabase.mdf";
            string _DBPath = Path.Combine(Environment.CurrentDirectory, _DBRelativePath);
            FileInfo _databaseFile = new(_DBPath);
            Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");
            //_connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True;Connect Timeout = 30;";
            // TODO: use relative path
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\zauro\\Documents\\studia\\4\\PT\\ProjectPT\\DataLayerTest\\Instrumentation\\TestDatabase.mdf;Integrated Security=True";
            _data = IData.New(IDataContext.New(_connectionString));
        }

        #region users

        [TestMethod]
        public async Task TestUsers()
        {
            using DatabaseDataContext context = new(_connectionString);

            try
            {
                // insert first test user

                string firstName1 = "Bartosz";
                string lastName1 = "Nowacki";
                string email1 = "bart@gmail.com";

                IUser user1 = await _data.CreateUser(firstName1, lastName1, email1);

                Assert.AreEqual("Bartosz", user1.FirstName);
                Assert.AreEqual(lastName1, user1.LastName);
                Assert.AreEqual(email1, user1.Email);

                // insert second test user

                string firstName2 = "Krzysztof";
                string lastName2 = "Muszynski";
                string email2 = "krzys@gmail.com";

                IUser user2 = await _data.CreateUser(firstName2, lastName2, email2);

                Assert.AreEqual(firstName2, user2.FirstName);
                Assert.AreEqual(lastName2, user2.LastName);
                Assert.AreEqual(email2, user2.Email);

                // test get users
                IEnumerable<IUser> users = await _data.GetUsers();

                Assert.AreEqual(2, users.Count());

                // test get user
                IUser fetchedUser = await _data.GetUser(user1.Id);

                Assert.AreEqual(user1.Id, fetchedUser.Id);
                Assert.AreEqual(user1.FirstName, fetchedUser.FirstName);
                Assert.AreEqual(user1.LastName, fetchedUser.LastName);
                Assert.AreEqual(user1.Email, fetchedUser.Email);
            }
            finally
            {
                context.TruncateAllData();
            }
        }

        /*
        [TestMethod]
        public async Task TestCreateUser()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "User not found")]
        public async void TestDeleteUser()
        {
            IEnumerable<IUser> users = await _data.GetUsers();
            if (users.Count() == 0)
            {
                throw new Exception("No users");
            }
            IUser p = users.First();
            Assert.IsTrue(await _data.DeleteUser(p.Id));
            await _data.GetUser(p.Id);
        }
        */

        #endregion users

        /*
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
        */
    }
}
