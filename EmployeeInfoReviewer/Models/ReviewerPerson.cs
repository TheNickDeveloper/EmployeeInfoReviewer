using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInfoReviewer.Models
{
    public class ReviewerPerson
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
        public List<ReviewerAddress> Addresses { get; set; } = new List<ReviewerAddress>();
        public List<ReviewerEmail> EmailAddresses { get; set; } = new List<ReviewerEmail>();
    }
}
