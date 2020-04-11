using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataAccessLibrary.DataAccess.Sql
{
    public class SqlServerPeopleContext : PeopleContext, IPeopleContext
    {
        public SqlServerPeopleContext(DbContextOptions options) : base(options)
        {
        }
    }
}
