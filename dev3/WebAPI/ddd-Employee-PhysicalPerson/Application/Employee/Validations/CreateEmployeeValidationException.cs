namespace ddd_Employee_PhysicalPerson.Application.Employee.Validations;

public class CreateEmployeeValidationException : Exception
{
    public IReadOnlyList<string> ValidationErrors { get; }
    public string ErrorMessage { get; }
    public int FailedId { get; }

    public CreateEmployeeValidationException(int employeeId, IEnumerable<string> errors)
    {
        ValidationErrors = errors.ToList();
        FailedId = employeeId;
        ErrorMessage = $"Validation failed to create employee with errors: {string.Join(" , ", ValidationErrors)}.";
    }
}