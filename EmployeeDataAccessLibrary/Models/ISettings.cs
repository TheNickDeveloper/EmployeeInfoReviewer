namespace EmployeeDataAccessLibrary.Models
{
    public interface ISettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string MongoConnectionName { get; set; }
    }
}