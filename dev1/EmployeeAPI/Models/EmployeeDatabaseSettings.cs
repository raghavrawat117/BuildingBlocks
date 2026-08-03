namespace EmployeeAPI.Models;

public class EmployeeDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string EmployeeCollectionName { get; set; } = null!;
}