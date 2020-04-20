using EmployeeInfoReviewer.Interfaces;
using Serilog;
using System;

namespace EmployeeInfoReviewer.Services
{
    public class LogHelper
    {
        private readonly IControllerLog _actionTaskNameHandler;
        private readonly string _className;

        public LogHelper(string className, IControllerLog actionTaskNameHandler)
        {
            _className = className;
            _actionTaskNameHandler = actionTaskNameHandler;
        }


        public void GetTaskActionName(string actionName, string id = null)
        {
            var inputId = id ?? "NoId";
            Log.Information(_actionTaskNameHandler.ReturnTaskActionName(_className, actionName, inputId));
        }

        public void ReturnSuccessStatus()
        {
            Log.Information($"{_className}: Success. [{DateTime.UtcNow}]");
        }

        public void ReturnNoFoudStatus(string id)
        {
            Log.Warning($"{_className}: Cannot find person:{id}. [{DateTime.UtcNow}]");
        }

        public void ReturnBadRequestStatus()
        {
            Log.Warning($"{_className}: Bad request. [{DateTime.UtcNow}]");
        }

        public void ReturnUncontrolException(string exceptionDetails)
        {
            Log.Error($"{_className}: UnExpected situation. {exceptionDetails}. [{DateTime.UtcNow}]");
        }

    }
}
