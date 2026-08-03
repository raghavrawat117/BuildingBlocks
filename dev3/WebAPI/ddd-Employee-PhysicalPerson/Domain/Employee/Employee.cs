using System.Text.Json.Serialization;
namespace ddd_Employee_PhysicalPerson.Domain;
public class Employee
{
    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }

    [JsonPropertyName("physicalPersonId")]
    public int PhysicalPersonId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("grade")]
    public int Grade { get; set; }

    [JsonPropertyName("experience")]
    public int Experience { get; set; }

    [JsonPropertyName("phoneNumber")]
    public long PhoneNumber { get; set; }

    [JsonPropertyName("workEmail")]
    public string WorkEmail { get; set; } = string.Empty;
}

