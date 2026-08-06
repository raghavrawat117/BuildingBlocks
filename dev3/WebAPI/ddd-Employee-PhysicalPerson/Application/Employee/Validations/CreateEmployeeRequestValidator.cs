using ddd_Employee_PhysicalPerson.Application.Employee.Contracts;
using FluentValidation;

namespace ddd_Employee_PhysicalPerson.Application.Employee.Validations;
public class CreateEmployeeRequestValidator
    : AbstractValidator<CreateEmployeeRequest>
{
    // Similar to rule engine pattern.
    // Each rule from a list of rules is applied in a loop format.
    // Error in even 1 breaks fails the loop.
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage("EmployeeId must be greater than 0.");

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Salary cannot be negative.");

        RuleFor(x => x.PhoneNumber)
            .Must(x => x.ToString().Length == 11)
            .WithMessage("PhoneNumber must be exactly 11 digits.");

        RuleFor(x => x.WorkEmail)
            .NotEmpty()
            .EmailAddress()
            .Must(x => x.EndsWith("@laminar.com",
                StringComparison.OrdinalIgnoreCase))
            .WithMessage("WorkEmail must end with @laminar.com.");
    }
}