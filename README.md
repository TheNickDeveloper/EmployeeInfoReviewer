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
7. Serilog.Extensions.Logging.File for export log file.

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

* Can switch either Sql-Server, Sqlite, or MongoDB as Database from back end.

* Dotnet core can intialize the DbContext by using dependency injection.

* When using Sql-Server, using functions in Startup and PeopleController below.

```csharp
    // Startup.cs
    services.AddDbContext<SqlServerPeopleContext>(options =>
    {
        options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
    });
    
    // PeopleController.cs
    public PeopleController(SqlServerPeopleContext context)
    {
        _peopleService = new PeopleService(context);
    }
```

* When using Sqlite, using functions in Startup and PeopleController below.

```csharp
    // Startup.cs
    services.AddDbContext<SqlitePeopleContext>(options =>
    {
        options.UseSqlite(Configuration.GetConnectionString("Sqlite"));
    });
    
    // PeopleController.cs
    public PeopleController(SqlitePeopleContext context)
    {
        _peopleService = new PeopleService(context);
    }
```

* When using MongoDB, using functions in Startup and PeopleController below.

```csharp
    // Startup.cs
    services.AddSingleton<IConfiguration>(Configuration);

    // PeopleController.cs
    public PeopleController(PeopleContext context, IConfiguration iconfig)
    {
        _peopleService = new MgPeopleService(iconfig);
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

