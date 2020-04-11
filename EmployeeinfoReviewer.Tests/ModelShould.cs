using EmployeeDataAccessLibrary.Models;
using System;
using Xunit;

namespace EmployeeinfoReviewer.Tests
{
    public class ModelShould
    {
        [Fact]
        public void BeEmptyWhenCreate_Person()
        {
            var person = new Person();
            Assert.NotNull(person);
        }

    }
}
