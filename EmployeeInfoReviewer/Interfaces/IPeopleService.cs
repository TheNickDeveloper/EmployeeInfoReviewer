using System.Collections.Generic;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Models;

namespace EmployeeInfoReviewer.Interfaces
{
    public interface IPeopleService
    {
        bool Delete(int id);
        IEnumerable<ReviewerPerson> Get();
        ReviewerPerson Get(int id);
        void Post(ReviewerPerson person);
        string Update(int id, ReviewerPerson person);
    }
}