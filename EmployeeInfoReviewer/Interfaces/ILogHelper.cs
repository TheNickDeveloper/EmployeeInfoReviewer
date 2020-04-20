namespace EmployeeInfoReviewer.Interfaces
{
    public interface ILogHelper
    {
        string _className { get; set; }
        IControllerLog _actionTaskNameHandler { get; set; }
        void GetTaskActionName(string actionName, string id = null);
        void ReturnBadRequestStatus();
        void ReturnNoFoudStatus(string id);
        void ReturnSuccessStatus();
        void ReturnUncontrolException(string exceptionDetails);
    }
}