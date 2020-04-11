using EmployeeDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataAccessLibrary.DataAccess.Sql
{
    public interface IPeopleContext
    {
        DbSet<Person> People { get; set; }
        DbSet<Email> EmailsAddresses { get; set; }
        DbSet<Address> Addresses { get; set; }
    }
}