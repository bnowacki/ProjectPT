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
    public class UserTest
    {
        [TestMethod]
        public void User_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedSurname = "Doe";
            string expectedName = "John";
            int expectedPhone = 123456789;
            string expectedEmail = "john.doe@example.com";

            // Act
            Customer user = new Customer(expectedSurname, expectedName, expectedPhone, expectedEmail, 1, 0); // Creating a Customer instance for testing

            // Assert
            Assert.AreEqual(expectedSurname, user.GetSurname());
            Assert.AreEqual(expectedName, user.GetName());
            Assert.AreEqual(expectedPhone, user.GetPhone());
            Assert.AreEqual(expectedEmail, user.GetEmail());
        }

        [TestMethod]
        public void User_SetSurname_ChangesSurnameCorrectly()
        {
            // Arrange
            Customer user = new Customer("Doe", "John", 123456789, "john.doe@example.com", 1, 0); // Creating a Customer instance for testing
            string newSurname = "Smith";

            // Act
            user.SetSurname(newSurname);

            // Assert
            Assert.AreEqual(newSurname, user.GetSurname());
        }

        [TestMethod]
        public void User_SetName_ChangesNameCorrectly()
        {
            // Arrange
            Customer user = new Customer("Doe", "John", 123456789, "john.doe@example.com", 1, 0); // Creating a Customer instance for testing
            string newName = "Alice";

            // Act
            user.SetName(newName);

            // Assert
            Assert.AreEqual(newName, user.GetName());
        }

        [TestMethod]
        public void User_SetPhone_ChangesPhoneCorrectly()
        {
            // Arrange
            Customer user = new Customer("Doe", "John", 123456789, "john.doe@example.com", 1, 0); // Creating a Customer instance for testing
            int newPhone = 987654321;

            // Act
            user.SetPhone(newPhone);

            // Assert
            Assert.AreEqual(newPhone, user.GetPhone());
        }

        [TestMethod]
        public void User_SetEmail_ChangesEmailCorrectly()
        {
            // Arrange
            Customer user = new Customer("Doe", "John", 123456789, "john.doe@example.com", 1, 0); // Creating a Customer instance for testing
            string newEmail = "john.smith@example.com";

            // Act
            user.SetEmail(newEmail);

            // Assert
            Assert.AreEqual(newEmail, user.GetEmail());
        }
    }
}
