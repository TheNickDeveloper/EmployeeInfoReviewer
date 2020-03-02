# EmployeeInfoReviewer
ASP.NET Core Web API project by using SQL Server and MongoDB. Basic CRUD practice by using instace of employee information.
 
## Farmework Apply
1. Asp.net Core web API.
2. Entity Framework Core.
3. SqlServer local.
4. MongoDB Driver.
5. MongoDB local.

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
* Basic CRUD.(Create, Read, Update, and Delete)

* Can choose either MS_SQL or MongoDB.

* Also support child object updating funciton.

* When using MS_SQL, using the contracture below. In addition, need to specify the child object ID while doing put function.

```csharp
    public PeopleController(PeopleContext context, IConfiguration iconfig)
    {
        _peopleService = new PeopleService(context);
    }
```

* When using MongoDB, using the contracture below. Child object ID will auto generate while doing put function.

```csharp
    public PeopleController(PeopleContext context, IConfiguration iconfig)
    {
        _peopleService = new MgPeopleService(iconfig);
    }
```
        
## How to Use
1. Pull application to local.
2. Make sure you have install SqlSever local for local db establish.
3. Makue sure you have installed the MongoDB, then lanuch it.
4. Lanuch application, and following the provide URL for doing CRUD.
