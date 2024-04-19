using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CoffeShop;
using CoffeShop.Data.Catalog;

namespace CoffeShopTest.DataTests.CatalogTests
{
    [TestClass]
    public class CatalogTest
    {
        [TestMethod]
        public void TestCatalogGetProducts()
        {
            Catalog catalog = new Catalog();

            Product product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            Product product2 = new Product(2, "Cookie", 0.75f, "Desert");
            Product product3 = new Product(3, "Cappucino", 2.00f, "Coffe");

            catalog.AddProduct(product1);
            catalog.AddProduct(product2);
            catalog.AddProduct(product3);

            var products = catalog.GetProducts();

            Assert.AreEqual(3, products.Count);
            CollectionAssert.Contains(products, product1);
            CollectionAssert.Contains(products, product2);
            CollectionAssert.Contains(products, product3);
        }

        [TestMethod]
        public void TestCatalogAddProduct()
        {
            Catalog catalog = new Catalog();
            Product product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");

            catalog.AddProduct(product1);
            var products = catalog.GetProducts();

            Assert.AreEqual(1, products.Count);
            CollectionAssert.Contains(products, product1);
        }

        [TestMethod]
        public void TestCatalogRemoveProduct()
        {
            Catalog catalog = new Catalog();
            Product product1 = new Product(1, "SmallCoffee", 1.50f, "Coffee");
            Product product2 = new Product(2, "Cookie", 0.75f, "Desert");
            catalog.AddProduct(product1);
            catalog.AddProduct(product2);

            catalog.RemoveProduct(product1);
            var products = catalog.GetProducts();

            Assert.AreEqual(1, products.Count);
            CollectionAssert.DoesNotContain(products, product1);
            CollectionAssert.Contains(products, product2);
        }

        [TestMethod]
        public void TestCatalogEmpty()
        {
            Catalog catalog = new Catalog();

            var products = catalog.GetProducts();

            Assert.AreEqual(0, products.Count);
        }
    }
}
