using EmployeeDataAccessLibrary.DataAccess.NonSql;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class MgPeopleService : IPeopleService
    {
        private readonly MgPeopleContext _context;

        public MgPeopleService(IConfiguration config)
        {
            _context = new MgPeopleContext(config);
        }

        public bool Delete(int id)
        {
            var person = Builders<Person>.Filter.Eq("Id", id);
            _context.People.DeleteOne(person);

            return true;
        }

        public IEnumerable<Person> Get()
        {
            return _context.People.Find(x => true).ToList();
        }

        public Person Get(int id)
        {
            var person = Builders<Person>.Filter.Eq("Id", id);
            return _context.People.Find(person).FirstOrDefault();
        }

        public void Post(Person person)
        {
          
            if (_context.People.Find(x => true).Any())
            {
                var maxId = _context.People.Find(x => true).SortByDescending(d => d.Id).Limit(1).FirstOrDefault().Id;
                person.Id = maxId + 1;
            }
            else
            {
                person.Id = 1;
            }

            person = UpdateInputInfo(person);

            _context.People.InsertOne(person);
        }

        public string Update(int id, Person person)
        {
            if (_context.People.Find(x=>x.Id == id).Any())
            {
                person = UpdateInputInfo(person);
                _context.People.ReplaceOne(x => x.Id == id, person);
                return "Success";
            }

            return "NotFound";
        }

        private Person UpdateInputInfo(Person person)
        {
            for (int i = 0; i < person.EmailAddresses.Count; i++)
            {
                person.EmailAddresses[i].Id = i + 1;
            }

            for (int i = 0; i < person.Addresses.Count; i++)
            {
                person.Addresses[i].Id = i + 1;
            }

            return person;
        }
    }
}
