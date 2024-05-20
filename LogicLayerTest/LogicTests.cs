using DataLayer.API;
using LogicLayer.API;
using LogicLayerTest.FakeDataLayer;
using Moq;

namespace LogicLayerTest
{
    [TestClass]
    public class LogicTests
    {
        private ILogic _logic;
        private Mock<IData> _dataMock;

        [TestInitialize]
        public void Initialize()
        {
            _dataMock = new();
            _logic = ILogic.New(_dataMock.Object);
        }

        [TestMethod]
        public void TestCreateOrder()
        {
            User user = new()
            {
                Id = new Guid("17A85441-ABA7-4AEA-B83D-0D61A9977218"),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com"
            };

            Product p1 = new()
            {
                Id = new Guid("3869AC88-6D6B-46CD-8208-61F82002D49A"),
                Name = "Mocha Madness",
                Price = 9.75f,
                Stock = 10
            };
            Product p2 = new()
            {
                Id = new Guid("A2372FE4-F5AB-40D4-8078-4BF82E9D8D1B"),
                Name = "Java Jumpstart",
                Price = 11.99f,
                Stock = 8
            };
            Product p3 = new()
            {
                Id = new Guid("CCA14795-9BE2-4042-862B-E59C13F7A064"),
                Name = "Caffeine Craze",
                Price = 7.50f,
                Stock = 63
            };
            Dictionary<Guid, int> products = new() {
                { p1.Id, 2},
                { p2.Id, 1},
                { p3.Id, 4},
            };

            // mock setup 
            _dataMock.Setup(x => x.GetProduct(p1.Id)).ReturnsAsync(p1);
            _dataMock.Setup(x => x.GetProduct(p2.Id)).ReturnsAsync(p2);
            _dataMock.Setup(x => x.GetProduct(p3.Id)).ReturnsAsync(p3);

            // test
            _logic.CreateOrder(user.Id, products);

            // verify method calls
            _dataMock.Verify(x => x.UpdateProductStock(p1.Id, p1.Stock - 2), Times.Once());
            _dataMock.Verify(x => x.UpdateProductStock(p2.Id, p2.Stock - 1), Times.Once());
            _dataMock.Verify(x => x.UpdateProductStock(p3.Id, p3.Stock - 4), Times.Once());

            _dataMock.Verify(x => x.CreateOrder(user.Id, products, p1.Price * 2 + p2.Price * 1 + p3.Price * 4), Times.Once());
        }

        [TestMethod]
        public void TestTakeDelivery()
        {
            Product p1 = new()
            {
                Id = new Guid("3869AC88-6D6B-46CD-8208-61F82002D49A"),
                Name = "Mocha Madness",
                Price = 9.75f,
                Stock = 0
            };
            Product p2 = new()
            {
                Id = new Guid("A2372FE4-F5AB-40D4-8078-4BF82E9D8D1B"),
                Name = "Java Jumpstart",
                Price = 11.99f,
                Stock = 0
            };
            Product p3 = new()
            {
                Id = new Guid("CCA14795-9BE2-4042-862B-E59C13F7A064"),
                Name = "Caffeine Craze",
                Price = 7.50f,
                Stock = 0
            };

            Dictionary<Guid, int> products = new() {
                { p1.Id, 2},
                { p2.Id, 1},
                { p3.Id, 4},
            };

            // mock setup 
            _dataMock.Setup(x => x.GetProduct(p1.Id)).ReturnsAsync(p1);
            _dataMock.Setup(x => x.GetProduct(p2.Id)).ReturnsAsync(p2);
            _dataMock.Setup(x => x.GetProduct(p3.Id)).ReturnsAsync(p3);

            // test
            _logic.TakeDelivery(products);

            // verify method calls
            _dataMock.Verify(x => x.UpdateProductStock(p1.Id, p1.Stock + 2), Times.Once());
            _dataMock.Verify(x => x.UpdateProductStock(p2.Id, p2.Stock + 1), Times.Once());
            _dataMock.Verify(x => x.UpdateProductStock(p3.Id, p3.Stock + 4), Times.Once());
        }
    }
}