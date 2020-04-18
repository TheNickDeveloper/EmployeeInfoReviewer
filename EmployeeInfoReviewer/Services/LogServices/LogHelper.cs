using EmployeeInfoReviewer.Interfaces;
using System;

namespace EmployeeInfoReviewer.Services
{
    public class LogHelper : ILogHelper
    {
        public string ClassName { get; set; }
        public string DbName { get; set; }

        public IControllerLog ActionTaskNameHandler { get; set; }

        public string GetTaskActionName(string actionName, string id = null)
        {
            var inputId = id ?? "NoId";
            return ActionTaskNameHandler.ReturnTaskActionName(ClassName, actionName, inputId);
        }

        public string ReturnSuccessStatus()
        {
            return $"{ClassName}: Success. [{DateTime.UtcNow}]";
        }

        public string ReturnNoFoudStatus(string id)
        {
            return $"{ClassName}: Cannot find person:{id}. [{DateTime.UtcNow}]";
        }

        public string ReturnBadRequestStatus()
        {
            return $"{ClassName}: Bad request. [{DateTime.UtcNow}]";
        }

        public string ReturnUncontrolException()
        {
            return $"{ClassName}: UnExpected situation. [{DateTime.UtcNow}]";
        }

    }
}
