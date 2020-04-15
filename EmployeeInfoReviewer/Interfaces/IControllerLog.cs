namespace EmployeeInfoReviewer.Interfaces
{
    public interface IControllerLog
    {
        string ReturnTaskActionName(string className, string actionName, string id = null);
    }
}