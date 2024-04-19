using CoffeShop.Data.Catalog;
using CoffeShop.Data.Users;
using CoffeShop.Logic;
using CoffeShop.Logic.DataContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShopTest.LogicTests.DataContextTests
{
    [TestClass]
    public class GroceryShopTest
    {
        [TestMethod]
        public void TestGroceryShopInitialization()
        {
            double initialBalance = 100.00;
            Cafe shop = new Cafe(initialBalance);

            Assert.IsNotNull(shop.GetCatalog());
            Assert.IsNotNull(shop.GetUsers());
            Assert.AreEqual(initialBalance, shop.GetBalance());
        }

        [TestMethod]
        public void TestAddUser()
        {
            Cafe shop = new Cafe(100.00);
            Customer user = new Customer("Doe", "John", 123456789, "john@example.com", 1, 0); // Creating a Customer instance for testing

            shop.AddUser(user);

            CollectionAssert.Contains(shop.GetUsers(), user);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Cafe shop = new Cafe(100.00);
            Customer user = new Customer("Doe", "John", 123456789, "john@example.com", 1, 0); // Creating a Customer instance for testing
            shop.AddUser(user);

            shop.DeleteUser(user);

            CollectionAssert.DoesNotContain(shop.GetUsers(), user);
        }

        [TestMethod]
        public void TestAddProduct()
        {
            Cafe shop = new Cafe(100.00);
            Product product = new Product(1, "SmallCoffee", 1.50f, "Coffee");

            shop.AddProduct(product);

            CollectionAssert.Contains(shop.GetCatalog().GetProducts(), product);
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            Cafe shop = new Cafe(100.00);
            Product product = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            shop.AddProduct(product);

            shop.DeleteProduct(product);

            CollectionAssert.DoesNotContain(shop.GetCatalog().GetProducts(), product);
        }

        [TestMethod]
        public void TestUpdateGroceryShopState()
        {
            double initialBalance = 100.00;
            Cafe shop = new Cafe(initialBalance);

            Product product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            Product product2 = new Product(2, "Cookie", 0.75f, "Desert");
            shop.AddProduct(product1);
            shop.AddProduct(product2);

            Customer user = new Customer("Doe", "John", 123456789, "john@example.com", 1, 0); // Creating a Customer instance for testing
            shop.AddUser(user);

            State state = new State(shop);

            shop.DeleteProduct(product1);
            shop.DeleteUser(user);
            shop.AddProduct(new Product(3, "IcedLatte", 2.00f, "Coffee"));

            shop.UpdateCafeState(state);

            int expectedProductCount = state.GetCurrentCatalog().GetProducts().Count;
            int actualProductCount = shop.GetCatalog().GetProducts().Count;
            Assert.AreEqual(expectedProductCount, actualProductCount, $"Expected {expectedProductCount} products, but found {actualProductCount} products.");

            int expectedUserCount = state.GetCurrentUsers().Count;
            int actualUserCount = shop.GetUsers().Count;
            Assert.AreEqual(expectedUserCount, actualUserCount, $"Expected {expectedUserCount} users, but found {actualUserCount} users.");

            Assert.AreEqual(state.GetCurrentBalance(), shop.GetBalance());
        }

    }
}
