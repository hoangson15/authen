using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ThisGameIsSoFun.Models
{
    public class MongoStudents
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Class { get; set; }
    }
}
