using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeinfoReviewer.Tests.PeopleControllersTests
{
    public class MockPeopleService : IPeopleService
    {
        private readonly List<ReviewerPerson> _reviewerPeople;
        private readonly List<ReviewerEmail> _reviewerEmail;
        private readonly List<ReviewerAddress> _reviewerAddress;

        public MockPeopleService()
        {
            _reviewerEmail = new List<ReviewerEmail>
            {
                new ReviewerEmail{ Id = 1, EmailAddress = "1@1" },
                new ReviewerEmail{ Id = 2, EmailAddress = "2@2" }
            };

            _reviewerAddress = new List<ReviewerAddress>
            {
                new ReviewerAddress{ Id = 1, City = "GZ", State = "GD", StreetAddress = "AD1",ZipCode = "G01" },
                new ReviewerAddress{ Id = 2, City = "KH", State = "TW", StreetAddress = "AD2",ZipCode = "T01" },
            };

            _reviewerPeople = new List<ReviewerPerson>
            {
                new ReviewerPerson
                {
                    Id = 1,
                    FirstName = "Nick",
                    LastName = "Tsai",
                    Age = 29,
                    EmailAddresses = _reviewerEmail,
                    Addresses = _reviewerAddress
                },
                new ReviewerPerson
                {
                    Id = 2,
                    FirstName = "Mina",
                    LastName = "Yang",
                    Age = 14,
                    EmailAddresses = _reviewerEmail,
                    Addresses = _reviewerAddress
                }
            };
        }

        public bool Delete(int id)
        {
            var removeTarget = _reviewerPeople.Where(x => x.Id == id).First();

            if (removeTarget != null)
            {
                _reviewerPeople.Remove(removeTarget);
                return true;
            }
            return false;
        }

        public IEnumerable<ReviewerPerson> Get()
        {
            return _reviewerPeople;
        }

        public ReviewerPerson Get(int id)
        {
            return _reviewerPeople.Where(x => x.Id == id).First();
        }

        public void Post(ReviewerPerson person)
        {
            _reviewerPeople.Add(person);
        }

        public string Update(int id, ReviewerPerson person)
        {
            var beReplaceTarget = _reviewerPeople.Where(x => x.Id == id).First();
            if (beReplaceTarget != null)
            {
                _reviewerPeople.Remove(beReplaceTarget);
                _reviewerPeople.Add(person);
                return "Success";
            }
            return "NotFound";
        }
    }
}
