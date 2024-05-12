using DataLayer;
using DataLayer.Catalog;
using DataLayer.State;
using DataLayer.Users;
using LogicLayer;
using Moq;
using System.Security.Cryptography;

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
            Customer customer = new()
            {
                Id = new Guid("17A85441-ABA7-4AEA-B83D-0D61A9977218"),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                Phone = "+9988776655",
                ShippingAddress = "789 Oak St, Anothertown, USA"
            };

            Product p1 = new()
            {
                Id = new Guid("3869AC88-6D6B-46CD-8208-61F82002D49A"),
                Name = "Mocha Madness",
                Description = "Indulge in the madness of our delicious mocha creation.",
                Brand = "Mocha Magic",
                Category = ProductCategory.Coffee,
                Price = 9.75f
            };
            Product p2 = new()
            {
                Id = new Guid("A2372FE4-F5AB-40D4-8078-4BF82E9D8D1B"),
                Name = "Java Jumpstart",
                Description = "Get a jumpstart on your day with the bold flavors of Java.",
                Brand = "Java Junction",
                Category = ProductCategory.Coffee,
                Price = 11.99f
            };
            Product p3 = new()
            {
                Id = new Guid("CCA14795-9BE2-4042-862B-E59C13F7A064"),
                Name = "Caffeine Craze",
                Description = "Join the craze with our irresistible caffeine-packed blend.",
                Brand = "Caffeine Co.",
                Category = ProductCategory.Coffee,
                Price = 7.50f
            };
            Dictionary<Guid, int> products = new() {
                { p1.Id, 2},
                { p2.Id, 1},
                { p3.Id, 4},
            };
            string shippingAddress = "Wólczańska 215, Łódź";

            // mock setup 
            _dataMock.Setup(x => x.GetProduct(p1.Id)).Returns(p1);
            _dataMock.Setup(x => x.GetProduct(p2.Id)).Returns(p2);
            _dataMock.Setup(x => x.GetProduct(p3.Id)).Returns(p3);

            _dataMock.Setup(x => x.GetUser(customer.Id)).Returns(customer);

            // test
            _logic.CreateOrder(customer.Id, products, shippingAddress);

            // verify method calls
            _dataMock.Verify(x => x.AddProductToStock(p1.Id, -2), Times.Once());
            _dataMock.Verify(x => x.AddProductToStock(p2.Id, -1), Times.Once());
            _dataMock.Verify(x => x.AddProductToStock(p3.Id, -4), Times.Once());

            _dataMock.Verify(x => x.CreateOrder(It.IsAny<Order>()), Times.Once());
        }

        [TestMethod]
        public void TestTakeDelivery()
        {
            Guid p1 = Guid.NewGuid();
            Guid p2 = Guid.NewGuid();
            Guid p3 = Guid.NewGuid();

            Dictionary<Guid, int> products = new() {
                { p1, 2},
                { p2, 1},
                { p3, 4},
            };

            // test
            _logic.TakeDelivery(products);

            // verify method calls
            _dataMock.Verify(x => x.AddProductToStock(p1, 2), Times.Once());
            _dataMock.Verify(x => x.AddProductToStock(p2, 1), Times.Once());
            _dataMock.Verify(x => x.AddProductToStock(p3, 4), Times.Once());
        }
    }
}