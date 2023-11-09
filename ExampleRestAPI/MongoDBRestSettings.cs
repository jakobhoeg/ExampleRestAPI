namespace ExampleRestAPI
{
    public class MongoDBRestSettings
    {


        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string PackageCollectionName { get; set; } = null;

        public string TestENVVar { get; set; } = null;
    }
}
