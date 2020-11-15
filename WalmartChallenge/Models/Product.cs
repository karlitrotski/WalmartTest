using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WalmartChallenge.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int id { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int price { get; set; }
    }
}