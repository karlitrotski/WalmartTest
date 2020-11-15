using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WalmartChallenge.Models;

namespace WalmartChallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            string text = model.search;
            var isNumeric = int.TryParse(text, out int n);
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["dbWalmart"].ToString();
            List<Product> result = null;
            MongoClient client = new MongoClient(conn);
            IMongoDatabase database = client.GetDatabase("WalmartChallenge");
            IMongoCollection<Product> collection = database.GetCollection<Product>("Products");
            if (isNumeric)
            {
                result = collection.Find(x =>
                    x.id == n ||
                    x.brand.Contains(text) ||
                    x.description.Contains(text)).ToList();
            } else if (text.Length >= 3)
            {
                result = collection.Find(x =>
                    x.brand.Contains(text) ||
                    x.description.Contains(text)).ToList();
            }
            model.isPalindrome = IsPalindrome(text);
            model.products = result;
            return View(model);
        }

        public bool IsPalindrome(string text)
        {
            char[] aText = text.ToCharArray();
            Array.Reverse(aText);
            string invText = new string(aText);
            return text.Equals(invText);
        }
    }
}