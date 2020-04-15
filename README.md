# EmployeeInfoReviewer
ASP.NET Core API feat Angular8 by using SQL Server and MongoDB. An web app that use employee information as CURD practice. 
 
## Technique Apply
#### Web API
1. Asp.net Core web API.
2. Entity Framework Core.
3. SqlServer local.
4. Sqlite local.
5. MongoDB Driver.
6. MongoDB local.
7. [Serilog.Extensions.Logging.File](https://github.com/serilog/serilog-extensions-logging-file) for export log file.
8. [Autofac](https://github.com/autofac/Autofac) as DI framework

#### Web Page
1. Angular 8
2. Angular material

## Data structure
    * Person
        ** Id
        ** FirstName
        ** LastName
        ** Age
        ** Addresses
            ==> Id
            ==> StreetAddress
            ==> City
            ==> State
            ==> ZipeCode
        ** EmailAddresses
            ==> Id
            ==> EmailAddress

## Functions
#### Backend
* Basic CRUD(Create, Read, Update, and Delete) either via postman or provided web page.

* Can switch Sql-Server, Sqlite, or MongoDB as Db from back end by power of Autofac.

* Db can be switched by change the TargetDbName in constructure of Startup.cs.

```csharp
    public enum DbOptions
    {
        SqlServer,
        Sqlite,
        MongoDb
    }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        TargetDbName = DbOptions.SqlServer;
    }
```

#### Frontend
##### People Info View Page
* View all people infomation in linked DB.
* Could view details by clicking target's detail button on the page.
* Could delete or update as well by clicking corresponding button on the page.
* Could do key word search by entering key word in Filter seach bar.
* Could do sorting by click the column name from the table.

![image](https://github.com/TheNickDeveloper/EmployeeInfoReviewer/blob/master/images/PeopleInfoView.png)


##### Add Person View Page
* Could add new person into the DB by filling the following page after clicking AddPerson tab.
* Basic validation for Name as required, and email need to follow email format.

![image](https://github.com/TheNickDeveloper/EmployeeInfoReviewer/blob/master/images/AddPersonView.png)

