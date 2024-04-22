using CoffeShop.Data;
using CoffeeShopTest.TestDataGenerators;
using CoffeShop.Data.Users;

namespace CoffeShopTest.Data
{
    [TestClass]
    public class DataTest
    {
        private IData _data;

        [TestInitialize]
        public void Initialize()
        {
            _data = CoffeShop.Data.Data.New();
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
        public void TestRemoveUser()
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
            _data.DeleteUser(c.Id);
            _data.GetUser(c.Id);
        }
    }
}
