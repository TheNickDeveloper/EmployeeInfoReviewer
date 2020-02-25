using System.Collections.Generic;
using System.Linq;
using EmployeeDataAccessLibrary.Models;

namespace EmployeeInfoReviewer.Interfaces
{
    public interface IPeopleService
    {
        bool Delete(int id);
        IEnumerable<Person> Get();
        IQueryable<Person> Get(int id);
        void Post(Person person);
        string Update(int id, Person person);
    }
}