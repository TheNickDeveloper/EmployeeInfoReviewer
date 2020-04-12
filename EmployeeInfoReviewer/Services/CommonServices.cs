using EmployeeDataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class CommonServices
    {

        public int CountTotalRecord(List<Person> people)
        {
            return people.Count();
        }

        public int CountTotalAudltRecord(List<Person> people)
        {
            var targetPeople = people.Where(x => x.Age >= 18).ToList();
            return targetPeople.Count();
        }

    }
}
