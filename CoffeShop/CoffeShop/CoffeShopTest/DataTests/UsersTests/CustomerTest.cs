using CoffeShop.Data.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShopTest.DataTests.UsersTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void Customers_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedSurname = "Brown";
            string expectedName = "David";
            int expectedPhone = 123987456;
            string expectedEmail = "david.brown@example.com";
            int expectedCustomerId = 789;
            int expectedBalance = 100;

            // Act
            Customer customer = new Customer(expectedSurname, expectedName, expectedPhone, expectedEmail, expectedCustomerId, expectedBalance);

            // Assert
            Assert.AreEqual(expectedSurname, customer.GetSurname());
            Assert.AreEqual(expectedName, customer.GetName());
            Assert.AreEqual(expectedPhone, customer.GetPhone());
            Assert.AreEqual(expectedEmail, customer.GetEmail());
            Assert.AreEqual(expectedCustomerId, customer.GetCustomerId());
            Assert.AreEqual(expectedBalance, customer.GetBalance());
        }

        [TestMethod]
        public void Customers_SetCustomerId_ChangesCustomerIdCorrectly()
        {
            // Arrange
            Customer customer = new Customer("Brown", "David", 123987456, "david.brown@example.com", 789, 100);
            int newCustomerId = 456;

            // Act
            customer.SetCustomerId(newCustomerId);

            // Assert
            Assert.AreEqual(newCustomerId, customer.GetCustomerId());
        }

        [TestMethod]
        public void Customers_SetBalance_ChangesBalanceCorrectly()
        {
            // Arrange
            Customer customer = new Customer("Brown", "David", 123987456, "david.brown@example.com", 789, 100);
            int newBalance = 200;

            // Act
            customer.SetBalance(newBalance);

            // Assert
            Assert.AreEqual(newBalance, customer.GetBalance());
        }
    }
}
