using EmployeeDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataAccessLibrary.DataAccess.Sql
{
    public class PeopleContext : DbContext, IPeopleContext
    {
        public PeopleContext(DbContextOptions options) : base(options) {}

        public DbSet<Person> People { get; set; }
        public DbSet<Email> EmailsAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
