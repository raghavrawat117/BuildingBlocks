namespace ddd_Employee_PhysicalPerson.Domain.Entities;

public class CanonicalEnvelopeFactory
{
    public static CanonicalEnvelope<T> Create<T>
    (
        T payload, 
        string sourceSystem, 
        int schemaVersion
    ) 
        where T : IHasBusinessKey
    {
        return new CanonicalEnvelope<T>
        {
            BusinessKey = payload.GetBusinessKey(),
            ReceivedAtUtc = DateTime.UtcNow,
            SourceSystem = sourceSystem,
            SchemaVersion = schemaVersion,
            Payload = payload
        };
    }
}