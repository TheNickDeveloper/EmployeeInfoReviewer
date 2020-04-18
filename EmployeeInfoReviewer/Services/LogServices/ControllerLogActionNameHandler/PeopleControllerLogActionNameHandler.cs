using EmployeeInfoReviewer.Interfaces;
using System;

namespace EmployeeInfoReviewer.Services.LogControllers
{
    public class PeopleControllerLogActionNameHandler : IControllerLog
    {
        public string ReturnTaskActionName(string className, string actionName, string id = null)
        {
            var inputId = id ?? "NoId";

            switch (actionName)
            {
                case "GetPeople":
                    return $"{className}: Get all people data. [{DateTime.UtcNow}]";
                case "PostPerson":
                    return $"{className}: Add new person info. [{DateTime.UtcNow}]";
                case "GetPerson":
                    return $"{className}: Get person:{inputId} data. [{DateTime.UtcNow}]";
                case "PutPerson":
                    return $"{className}: Update person:{inputId} data. [{DateTime.UtcNow}]";
                case "DeletePerson":
                    return $"{className}: Delete person:{inputId} data. [{DateTime.UtcNow}]";
                default:
                    return $"{className}: Not in control. [{DateTime.UtcNow}]";
            }
        }
    }
}
