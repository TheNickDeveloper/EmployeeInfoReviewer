using Microsoft.EntityFrameworkCore;

namespace EmployeeDataAccessLibrary.DataAccess.Sql
{
    public class SqlitePeopleContext : PeopleContext, IPeopleContext
    {
        public SqlitePeopleContext(DbContextOptions options) : base(options)
        {
        }
    }
}
