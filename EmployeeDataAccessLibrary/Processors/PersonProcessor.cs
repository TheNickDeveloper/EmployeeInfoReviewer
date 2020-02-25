using EmployeeDataAccessLibrary.DataAccess;
using EmployeeDataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDataAccessLibrary.Processors
{
    public class PersonProcessor
    {

        private readonly PeopleContext _db;
        public PersonProcessor(PeopleContext db)
        {
            _db = db;
        }

        public void CreatePerson(int id, string firstName ,string lastName, List<Email> emails, List<Address> addresses)
        {
            var person = new Person()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                EmailAddresses = emails,
                Addresses = addresses
            };

            //using (var db = new PeopleContext())
            //{

            //}

            _db.People.Add(person);
            _db.SaveChanges();
        }

        public List<Person> ReadPerson()
        {
            var personList = new List<Person>();
            personList.AddRange(_db.People.Select(t => t));
            return personList;
        }
    }
}
