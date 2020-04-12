using System.Collections.Generic;
using EmployeeDataAccessLibrary.Models;

namespace EmployeeInfoReviewer.Interfaces
{
    public interface IPeopleService
    {
        bool Delete(int id);
        IEnumerable<Person> Get();
        Person Get(int id);
        void Post(Person person);
        string Update(int id, Person person);
    }
}