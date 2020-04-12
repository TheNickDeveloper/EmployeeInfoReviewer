using System;

namespace EmployeeInfoReviewer.Services
{
    public class LogHelper
    {
        private readonly string _className;

        public LogHelper(string className)
        {
            _className = className;
        }

        public string GetConnectionDb(string dbName)
        {
            return $"{_className}: Connect {dbName} as DB. [{DateTime.UtcNow}]";
        }

        public string GetTaskActionName(string actionName, string id = null)
        {
            var inputId = id ?? "NoId";

            switch (actionName)
            {
                case "GetPeople":
                    return $"{_className}: Get all people data. [{DateTime.UtcNow}]";
                case "GetPerson":
                    return $"{_className}: Get person:{inputId} data. [{DateTime.UtcNow}]";
                case "PostPerson":
                    return $"{_className}: Add new person info. [{DateTime.UtcNow}]";
                case "PutPerson":
                    return $"{_className}: Update person:{inputId} data. [{DateTime.UtcNow}]";
                case "DeletePerson":
                    return $"{_className}: Delete person:{inputId} data. [{DateTime.UtcNow}]";
                default:
                    return $"{_className}: Not in control. [{DateTime.UtcNow}]";
            }
        }

        public string ReturnSuccessStatus()
        {
            return $"{_className}: Success. [{DateTime.UtcNow}]";
        }

        public string ReturnNoFoudStatus(string id)
        {
            return $"{_className}: Cannot find person:{id}. [{DateTime.UtcNow}]";
        }

        public string ReturnBadRequestStatus()
        {
            return $"{_className}: Bad request. [{DateTime.UtcNow}]";
        }

        public string ReturnUncontrolException()
        {
            return $"{_className}: UnExpected situation. [{DateTime.UtcNow}]";
        }

    }
}
