using System.Text.Json.Serialization;
namespace ddd_Employee_PhysicalPerson.Domain.Entities;

public class PhysicalPersonEntity : IHasBusinessKey
{
    [JsonPropertyName("physicalPersonId")]
    public int PhysicalPersonId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("dateOfBirth")]
    public string DateOfBirth { get; set; }= string.Empty;

    [JsonPropertyName("phoneNumber")]
    public long PhoneNumber { get; set; }

    [JsonPropertyName("ssno")]
    public int Ssno { get; set; }

    public string GetBusinessKey()
    {
        return PhysicalPersonId.ToString();
    }
}

