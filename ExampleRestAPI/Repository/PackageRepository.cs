using ExampleRestAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExampleRestAPI.Repository
{

    public interface IPackageRepository
    {
        public Task Add(Package package);
        public Task Delete(Guid id);

        public Task<Package?> Get(Guid id);
        public Task<List<Package>> GetAll();

    }


    public class PackageRepository : IPackageRepository
    {
        private readonly IMongoCollection<Package> _packages;

        string envar = "";

        public PackageRepository(IOptions<MongoDBRestSettings> mongoDBRest)
        {
            var mongoClient = new MongoClient(mongoDBRest.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBRest.Value.DatabaseName);

            _packages = mongoDatabase.GetCollection<Package>(mongoDBRest.Value.PackageCollectionName);

            //envar = mongoDatabase.Value.TestENVVAR;
        }

        public async Task Add(Package package)
        {

            await _packages.InsertOneAsync(package);

        }

        public async Task<Package?> Get(Guid id)
        {
            return await _packages.Find(p => p.Id == id).FirstOrDefaultAsync();

        }

        public async Task<List<Package>> GetAll()
        {
            return await _packages.Find(p => true).ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _packages.DeleteOneAsync(p => p.Id == id);
        }




        #region Testenvvar
        //public async Task<Student?> Get1(Guid id)
        //{
        //    return await _students.Find(s => s.Id == id).FirstOrDefaultAsync();

        //    envar
        //}

        #endregion
    }

}
