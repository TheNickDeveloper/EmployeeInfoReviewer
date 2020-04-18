using AutoMapper;
using EmployeeDataAccessLibrary.DataAccess.NonSql;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class MongoDbPeopleService : IPeopleService
    {
        private readonly MongoDbPeopleContext _context;
        private readonly IMapper _mapper;

        public MongoDbPeopleService(IConfiguration config, IMapper mapper)
        {
            _context = new MongoDbPeopleContext(config);
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            var person = Builders<Person>.Filter.Eq("Id", id);
            _context.People.DeleteOne(person);

            return true;
        }

        public IEnumerable<ReviewerPerson> Get()
        {
            var sourcePeopleInfo = _context.People.Find(x => true).ToList();
            return _mapper.Map<List<ReviewerPerson>>(sourcePeopleInfo);
        }

        public ReviewerPerson Get(int id)
        {
            var person = Builders<Person>.Filter.Eq("Id", id);
            var targetPerson = _context.People.Find(person).FirstOrDefault();

            return _mapper.Map<ReviewerPerson>(targetPerson);
        }

        public void Post(ReviewerPerson person)
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
            var convertedPerson = _mapper.Map<Person>(person);

            _context.People.InsertOne(convertedPerson);
        }

        public string Update(int id, ReviewerPerson person)
        {
            if (_context.People.Find(x=>x.Id == id).Any())
            {
                person = UpdateInputInfo(person);
                var convertedPerson = _mapper.Map<Person>(person);
                _context.People.ReplaceOne(x => x.Id == id, convertedPerson);
                return "Success";
            }

            return "NotFound";
        }

        private ReviewerPerson UpdateInputInfo(ReviewerPerson person)
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
