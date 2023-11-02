using MongoDB.Bson.Serialization.Attributes;

namespace ExampleRestAPI.Model
{
    public class Student
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
    }

}
