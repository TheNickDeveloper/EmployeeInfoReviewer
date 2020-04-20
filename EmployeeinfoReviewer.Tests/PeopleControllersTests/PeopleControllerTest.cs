using EmployeeInfoReviewer.Controllers;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace EmployeeinfoReviewer.Tests.PeopleControllersTests
{
    public class PeopleControllerTest
    {
        private readonly IPeopleService _peopleService;
        private readonly PeopleController _controller;

        public PeopleControllerTest()
        {
            _peopleService = new MockPeopleService();
            _controller = new PeopleController(_peopleService);
        }

        [Fact]
        public async void ShouldReturnCorrectNumberInPeopleCollection()
        {
            var people = await _controller.GetPeople();
            var items = Assert.IsType<List<ReviewerPerson>>(people);
            Assert.Equal(2, items.Count);
        }

        [Theory]
        [InlineData(1, "Nick")]
        [InlineData(2, "Mina")]
        public async void ShoulfReturnTargetPerson(int id, string expectedName)
        {
            var person = await _controller.GetPerson(id) as OkObjectResult;
            var p = person.Value as ReviewerPerson;

            Assert.Equal(expectedName, p.FirstName);
        }

        [Theory]
        [InlineData(3, "Jack")]
        public async void ShouldReturnPostPersonResult(int id, string name)
        {
            var person = new ReviewerPerson
            {
                Id = id,
                FirstName = name
            };

            await _controller.PostPerson(person);

            var target = await _controller.GetPerson(3) as OkObjectResult; ;
            var targetPerson = target.Value as ReviewerPerson;

            Assert.Equal(name, targetPerson.FirstName);
        }

        [Fact]
        public async void ShouldUpdateTargetPerson()
        {
            var person = new ReviewerPerson
            {
                Id = 2,
                FirstName = "MinaNew"
            };

            await _controller.PutPerson(2, person);

            var target = await _controller.GetPerson(2) as OkObjectResult;
            var targetPerson = target.Value as ReviewerPerson;

            Assert.Equal("MinaNew", targetPerson.FirstName);
        }

        [Theory]
        [InlineData(2)]
        public async void ShouldDeleteTargetPerson(int id)
        {
            var isDelete = await _controller.DeletePerson(id);

            var people = await _controller.GetPeople() as List<ReviewerPerson>;
            var result = people.Find(x => x.Id == 2);
            Assert.Null(result);
        }

    }
}
