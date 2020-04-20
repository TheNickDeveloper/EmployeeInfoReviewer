using AutoMapper;
using EmployeeDataAccessLibrary.DataAccess.NonSql;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using EmployeeInfoReviewer.Services.LogControllers;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class MongoDbPeopleService : IPeopleService
    {
        private readonly MongoDbPeopleContext _context;
        private readonly IMapper _mapper;
        private readonly LogHelper _logHelper;

        public MongoDbPeopleService(IConfiguration config, IMapper mapper)
        {
            _context = new MongoDbPeopleContext(config);
            _mapper = mapper;
            _logHelper = new LogHelper("MongoDbPeopleService", new PeopleLogActionNameHandler());
        }

        public IEnumerable<ReviewerPerson> Get()
        {
            _logHelper.GetTaskActionName("GetPeople");
            var sourcePeopleInfo = _context.People.Find(x => true).ToList();
            var result = new List<ReviewerPerson>();

            try
            {
                result = _mapper.Map<List<ReviewerPerson>>(sourcePeopleInfo);
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }

            return result;
        }

        public ReviewerPerson Get(int id)
        {
            _logHelper.GetTaskActionName("GetPerson", id.ToString());

            var person = Builders<Person>.Filter.Eq("Id", id);
            var targetPerson = _context.People.Find(person).FirstOrDefault();
            if (targetPerson == null)
            {
                _logHelper.ReturnNoFoudStatus(id.ToString());
                return null;
            }

            var reviewerPerson = new ReviewerPerson();

            try
            {
                reviewerPerson = _mapper.Map<ReviewerPerson>(targetPerson);
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }

            return reviewerPerson;
        }

        public void Post(ReviewerPerson person)
        {
            _logHelper.GetTaskActionName("PostPerson");

            if (_context.People.Find(x => true).Any())
            {
                var maxId = _context.People.Find(x => true)
                    .SortByDescending(d => d.Id).Limit(1).FirstOrDefault().Id;
                person.Id = maxId + 1;
            }
            else
            {
                person.Id = 1;
            }

            person = UpdateInputInfoId(person);
            var convertedPerson = _mapper.Map<Person>(person);

            try
            {
                _context.People.InsertOne(convertedPerson);
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }
        }

        private ReviewerPerson UpdateInputInfoId(ReviewerPerson person)
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

        public string Update(int id, ReviewerPerson person)
        {
            _logHelper.GetTaskActionName("UpdatePerson", id.ToString());

            if (_context.People.Find(x=>x.Id == id).Any())
            {
                person = UpdateInputInfoId(person);
                var convertedPerson = _mapper.Map<Person>(person);

                try
                {
                    _context.People.ReplaceOne(x => x.Id == id, convertedPerson);
                    _logHelper.ReturnSuccessStatus();
                    return "Success";
                }
                catch (Exception e)
                {
                    _logHelper.ReturnUncontrolException(e.Message);
                    return "UnExpectError";
                }
            }

            _logHelper.ReturnNoFoudStatus(id.ToString());
            return "NotFound";
        }

        public bool Delete(int id)
        {
            _logHelper.GetTaskActionName("DeletePerson", id.ToString());

            var person = Builders<Person>.Filter.Eq("Id", id);

            try
            {
                _logHelper.ReturnSuccessStatus();
                _context.People.DeleteOne(person);
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
                return false;
            }

            return true;
        }
    }
}
