namespace EmployeeDataAccessLibrary.Models
{
    public class Settings : ISettings
    {

        public string MongoConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }

    }
}
