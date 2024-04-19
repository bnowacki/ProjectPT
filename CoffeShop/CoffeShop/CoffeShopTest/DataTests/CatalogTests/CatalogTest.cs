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

            Product product1 = new Product(1, "Apple", 1.50f, "Fruit");
            Product product2 = new Product(2, "Banana", 0.75f, "Fruit");
            Product product3 = new Product(3, "Tomato", 2.00f, "Vegetable");

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
            Product product1 = new Product(1, "Apple", 1.50f, "Fruit");

            catalog.AddProduct(product1);
            var products = catalog.GetProducts();

            Assert.AreEqual(1, products.Count);
            CollectionAssert.Contains(products, product1);
        }

        [TestMethod]
        public void TestCatalogRemoveProduct()
        {
            Catalog catalog = new Catalog();
            Product product1 = new Product(1, "Apple", 1.50f, "Fruit");
            Product product2 = new Product(2, "Banana", 0.75f, "Fruit");
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
