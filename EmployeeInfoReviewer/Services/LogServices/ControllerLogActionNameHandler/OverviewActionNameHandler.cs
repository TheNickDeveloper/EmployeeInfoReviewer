using EmployeeInfoReviewer.Interfaces;
using System;

namespace EmployeeInfoReviewer.Services.LogControllers
{
    public class OverviewActionNameHandler : IControllerLog
    {
        public string ReturnTaskActionName(string className, string actionName, string id = null)
        {
            switch (actionName)
            {
                case "GetTotalRecordNumber":
                    return $"{className}: Get total reocrd number from data. [{DateTime.UtcNow}]";
                case "GetAudltTotalRecordNumber":
                    return $"{className}: Get total audlit reocrd number from data. [{DateTime.UtcNow}]";
                default:
                    return $"{className}: Not in control. [{DateTime.UtcNow}]";
            }
        }
    }
}
