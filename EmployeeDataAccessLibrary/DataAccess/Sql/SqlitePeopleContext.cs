using EmployeeDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDataAccessLibrary.DataAccess.Sql
{
    public class SqlitePeopleContext : PeopleContext, IPeopleContext
    {
        public SqlitePeopleContext(DbContextOptions options) : base(options)
        {
        }
    }
}
