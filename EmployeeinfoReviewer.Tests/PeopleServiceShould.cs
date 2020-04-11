using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Services;
using System.Collections.Generic;
using Xunit;

namespace EmployeeinfoReviewer.Tests
{
    public class PeopleServiceShould
    {
        [Fact]
        public void CalculateTotalNumberOfPeople()
        {
            var service = new CommonServices();
            var people = new List<Person>();

            var count = 10;
            for (int i = 0; i < count; i++)
            {
                var person = new Person { Id = i };
                people.Add(person);
            }

            Assert.Equal(count, service.CountTotalRecord(people));
        }

        [Fact]
        public void CalculateTotalNumberOfAuldtPeople()
        {
            var service = new CommonServices();
            var people = new List<Person>();

            var count = 20;
            for (int i = 0; i < count; i++)
            {
                var person = new Person { Id = i, Age = i };
                people.Add(person);
            }

            Assert.Equal(2, service.CountTotalAudltRecord(people));
        }

    }
}
