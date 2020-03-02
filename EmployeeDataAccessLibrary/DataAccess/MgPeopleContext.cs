﻿using EmployeeDataAccessLibrary.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace EmployeeDataAccessLibrary.DataAccess
{
    public class MgPeopleContext
    {
        private readonly IMongoDatabase _db = null;
        private readonly IConfiguration config;

        public MgPeopleContext(IConfiguration iconfig)
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
