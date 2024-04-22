using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CoffeShop;
using CoffeShop.Data.Catalog;

namespace CoffeShopTest.DataTests.CatalogTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestProductProperties()
        {
            int productId = 1;
            string productName = "SmallCoffee";
            float productPrice = 1.50f;
            string productType = "Coffee";

            Product product = new Product(productId, productName, productPrice, productType);

            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual(productName, product.GetName());
            Assert.AreEqual(productPrice, product.GetPrice());
            Assert.AreEqual(productType, product.GetTypeOfProdcut());
        }

        [TestMethod]
        public void TestSetPrice()
        {
            int productId = 1;
            string productName = "SmallCoffee";
            float productPrice = 1.50f;
            string productType = "Coffee";
            Product product = new Product(productId, productName, productPrice, productType);

            float newPrice = 2.00f;
            product.SetPrice(newPrice);

            Assert.AreEqual(newPrice, product.GetPrice());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSetNegativePrice()
        {
            int productId = 1;
            string productName = "SmallCoffee";
            float productPrice = 1.50f;
            string productType = "Coffee";
            Product product = new Product(productId, productName, productPrice, productType);

            product.SetPrice(-1.00f);

        }
    }
}
