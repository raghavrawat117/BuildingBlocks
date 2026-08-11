namespace ddd_Employee_PhysicalPerson.Application.PhysicalPerson.Validations;

public class CreatePhysicalPersonValidationException : Exception
{
    public IReadOnlyList<string> ValidationErrors { get; }
    public string ErrorMessage { get; }
    public int FailedId { get; }

    public CreatePhysicalPersonValidationException(int physicalPersonId, IEnumerable<string> errors)
    {
        ValidationErrors = errors.ToList();
        FailedId = physicalPersonId;
        ErrorMessage = $"Validation failed to create physical person with errors: {string.Join(" , ", ValidationErrors)}.";
    }
}