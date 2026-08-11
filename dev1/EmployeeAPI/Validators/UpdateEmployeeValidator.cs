using EmployeeAPI.DTOs;
using FluentValidation;

namespace EmployeeAPI.Validators;

public class UpdateEmployeeValidator
    : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(60);

        RuleFor(x => x.Department)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(Email => Email.EndsWith("@capgemini.com", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Only Company emails are allowed");

        RuleFor(x => x.Salary)
            .GreaterThan(0);
    }
}