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

namespace CoffeShopTest.LogicTests
{
    public class EventsTest
    {
        [TestMethod]
        public void SellProducts_ProductNotInCatalog_ThrowsException()
        {
            // Arrange
            // Create a catalog without any products
            var catalog = new Catalog();
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();
            var product = new Product(1, "SmallCoffee", 1.50f, "Coffee");

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => events.SellProducts(new[] { product }, state));
        }

        [TestMethod]
        public void SellProducts_ProductInCatalog_DeductsTotalPriceFromBalance_RemovesProductsFromCatalog()
        {
            // Arrange
            var catalog = new Catalog();
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();
            var product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            var product2 = new Product(2, "Cookie", 0.75f, "Desert");

            // Add products to the catalog
            catalog.AddProduct(product1);
            catalog.AddProduct(product2);
            state.SetCurrentCatalog(catalog);

            // Act
            events.SellProducts(new[] { product1, product2 }, state);

            // Assert
            Assert.AreEqual(998.98f, state.GetCurrentBalance()); // $1000 - $1.99 - $0.99
            Assert.IsFalse(state.GetCurrentCatalog().GetProducts().Any()); // Catalog should be empty
        }

        [TestMethod]
        public void TakeDelivery_AddsProductsToCatalog()
        {
            // Arrange
            var catalog = new Catalog();
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();
            var product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            var product2 = new Product(2, "Cookie", 0.75f, "Desert");
            var products = new[] { product1, product2 };

            // Act
            events.TakeDelivery(products, state);

            // Assert
            Assert.AreEqual(2, state.GetCurrentCatalog().GetProducts().Count); // Two products should be added
        }

        [TestMethod]
        public void PayEmployee_EmployeeNotInCurrentUsers_ThrowsException()
        {
            // Arrange
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();
            var employee = new Employee("Doe", "John", 123456789, "john.doe@example.com", 1, 1000); // Employee not added to state

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => events.PayEmployee(employee, state));
        }

        [TestMethod]
        public void PayEmployee_EmployeeInCurrentUsers_DeductsSalaryFromBalance()
        {
            // Arrange
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();
            var employee = new Employee("Doe", "John", 123456789, "john.doe@example.com", 1, 1000);
            state.GetCurrentUsers().Add(employee);

            // Act
            events.PayEmployee(employee, state);

            // Assert
            Assert.AreEqual(0, state.GetCurrentBalance()); // Balance should be zero after paying the employee
        }

        [TestMethod]
        public void PayTaxes_DeductsTaxAmountFromBalance()
        {
            // Arrange
            var shop = new Cafe(1000);
            var state = new State(shop);
            var events = new Events();

            // Act
            events.PayTaxes(200, state); // Deduct $200 for taxes

            // Assert
            Assert.AreEqual(800, state.GetCurrentBalance()); // Balance should be $800 after paying taxes
        }
    }

}
