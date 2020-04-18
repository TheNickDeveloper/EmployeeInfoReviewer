using EmployeeDataAccessLibrary.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace EmployeeDataAccessLibrary.DataAccess.NonSql
{
    public class MongoDbPeopleContext
    {
        private readonly IMongoDatabase _db = null;
        private readonly IConfiguration config;

        public MongoDbPeopleContext(IConfiguration iconfig)
        {
            config = iconfig;
            var client = new MongoClient(config.GetSection("MongoConnection").GetSection("ConnectionString").Value);

            if (client != null)
            {
                _db = client.GetDatabase(config.GetSection("MongoConnection").GetSection("Database").Value);
            }
        }


        public IMongoCollection<Person> People
        {
            get
            {
                return _db.GetCollection<Person>("Person");
            }
        }

    }
}
