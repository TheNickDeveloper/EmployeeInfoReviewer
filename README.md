# EmployeeInfoReviewer
ASP.NET Core Web API project. Basic CRUD practice by using instace of employee information.
 
## Farmework Apply
1. Asp.net Core web API.
2. Entity Framework Core code first apply for dtatabase initilization.
3. SqlServer local.

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
1. Basic CRUD(Create, Read, Update, and Delete).
2. Can add, delete, or update for child object(Address/EmailAddresses) while doing put(update) action.

            
## How to Use
1. Pull application to local.
2. Lanuch application, and make sure you have install SqlSever local for local db establish.
3. Following the provide URL for doing CRUD.
