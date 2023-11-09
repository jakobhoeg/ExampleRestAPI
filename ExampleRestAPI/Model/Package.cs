using MongoDB.Bson.Serialization.Attributes;

namespace ExampleRestAPI.Model
{
    public class Package
    {
        [BsonId]
        public Guid Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

}
