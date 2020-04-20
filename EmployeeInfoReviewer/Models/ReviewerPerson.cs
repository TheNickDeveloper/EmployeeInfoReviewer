using System.Collections.Generic;

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
