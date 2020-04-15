namespace EmployeeInfoReviewer.Interfaces
{
    public interface ILogHelper
    {
        string ClassName { get; set; }
        string DbName { get; set; }
        IControllerLog ActionTaskNameHandler { get; set; }
        string GetTaskActionName(string actionName, string id = null);
        string ReturnBadRequestStatus();
        string ReturnNoFoudStatus(string id);
        string ReturnSuccessStatus();
        string ReturnUncontrolException();
    }
}