using System.Text.Json.Serialization;

namespace Scehmas { 
    
    public class Employee
    {
        [JsonPropertyName("empId")]
        public int EmpId { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("grade")]
        public Grade Grade { get; set; }

        [JsonPropertyName("experience")]
        public int Experience { get; set; }

        [JsonPropertyName("phoneNumber")]
        public long PhoneNumber { get; set; }

        [JsonPropertyName("workEmail")]
        public string WorkEmail { get; set; } = string.Empty;
    }

    public enum Grade
    {
        Junior,
        MidLevel,
        Senior,
        Lead
    }
}