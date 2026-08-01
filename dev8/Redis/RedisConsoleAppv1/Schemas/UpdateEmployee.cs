using System.Text.Json.Serialization;

namespace Scehmas
{
    public class UpdateEmployee
    {
        [JsonPropertyName("empId")]
        public int EmpId { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("grade")]
        public Grade? Grade { get; set; }

        [JsonPropertyName("experience")]
        public int? Experience { get; set; }

        [JsonPropertyName("phoneNumber")]
        public long? PhoneNumber { get; set; }

        [JsonPropertyName("workEmail")]
        public string? WorkEmail { get; set; }
    }

}
