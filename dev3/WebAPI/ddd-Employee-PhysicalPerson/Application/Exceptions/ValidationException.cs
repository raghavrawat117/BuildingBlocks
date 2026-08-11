namespace ddd_Employee_PhysicalPerson.Application;

public class ValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }
}