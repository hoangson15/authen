using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ThisGameIsSoFun.Infrastructures.Config;
using ThisGameIsSoFun.Infrastructures.Models;
using ThisGameIsSoFun.Models;

namespace ThisGameIsSoFun.Services
{
    public interface IMongoTestService
    {
        Task<List<MongoStudents>> GetAllStudents();
        Task InsertStudents();
    }
    public class MongoTestService : IMongoTestService
    {
        private readonly IMongoClient client;
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MongoStudents> _students;

        public MongoTestService(IMongoClient client, IConfiguration configuration)
        {
            this.client = client;
            //var database = client.GetDatabase(config.Value.DatabaseName);
            _configuration = configuration;
            var databaseName = _configuration.GetValue<string>("MongoDbConfig:DatabaseName");
            _database = client.GetDatabase(databaseName);
            _students = _database.GetCollection<MongoStudents>("Students");
        }

        public async Task<List<MongoStudents>> GetAllStudents()
        {
            try
            {
                
                return await _students.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<MongoStudents>();
            }
        }

        public async Task InsertStudents()
        {
            try
            {
                List<MongoStudents> insertList = new List<MongoStudents>
                {
                    new MongoStudents { Name = "Alice", Age = "18", Id = "4", Class = "12A8" },
                };
                _students.InsertMany(insertList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
