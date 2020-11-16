using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WalmartChallenge.Models;
using WalmartChallenge.Controllers;
using MongoDB.Driver;

namespace WalmartChallenge.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IsPalindrome()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            bool result = controller.IsPalindrome("abba");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotPalindrome()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            bool result = controller.IsPalindrome("abab");

            // Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void GetDataFromMongoDB()
        {
            string text = "azwan ubdehk";
            var isNumeric = int.TryParse(text, out int n);
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["dbWalmart"].ToString();
            List<Product> result = null;
            MongoClient client = new MongoClient(conn);
            IMongoDatabase database = client.GetDatabase("WalmartChallenge");
            IMongoCollection<Product> collection = database.GetCollection<Product>("Products");
            result = collection.Find(x =>
                    x.brand.Contains(text) ||
                    x.description.Contains(text)).ToList();
            Assert.IsNotNull(result);
        }
    }
}
