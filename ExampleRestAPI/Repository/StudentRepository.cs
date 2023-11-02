using ExampleRestAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExampleRestAPI.Repository
{

    public interface IStudentRepository
    {
        public Task Add(Student student);

        public Task<Student?> Get(Guid id);
    }


    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        string envar = "";

        public StudentRepository(IOptions<MongoDBRestSettings> mongoDBRest)
        {
            var mongoClient = new MongoClient(mongoDBRest.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBRest.Value.DatabaseName);

            _students = mongoDatabase.GetCollection<Student>(mongoDBRest.Value.StudentsCollectionName);

            //envar = mongoDatabase.Value.TestENVVAR;
        }

        public async Task Add(Student student)
        {
            await _students.InsertOneAsync(student);
        }

        public async Task<Student?> Get(Guid id)
        {
            return await _students.Find(s => s.Id == id).FirstOrDefaultAsync();

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
