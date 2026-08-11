using System.Text.Json;
using System.Text.Json.Serialization;

namespace ddd_Employee_PhysicalPerson.Domain.Entities;

public class CanonicalEnvelope<T>
{
    /// <summary>
    /// EntityId or BusinessKey
    /// </summary>
    public string BusinessKey { get; set; } = string.Empty;

    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime ReceivedAtUtc { get; set; }

    public string SourceSystem { get; set; } = string.Empty;

    public int SchemaVersion { get; set; }

    public T Payload { get; set; } = default!;

    // // Implement Later
    // public Guid CorrelationId { get; set; }
}

// This is a datetime helper to store data in the required format
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "dd-MM-yyyy hh:mm:ss tt";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()!, Format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, System.Globalization.CultureInfo.InvariantCulture));
    }
}