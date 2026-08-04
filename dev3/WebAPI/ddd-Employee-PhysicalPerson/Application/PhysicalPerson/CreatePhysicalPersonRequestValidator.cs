using ddd_Employee_PhysicalPerson.Application;
using FluentValidation;

public class CreatePhysicalPersonRequestValidator
    : AbstractValidator<CreatePhysicalPersonRequest>
{
    public CreatePhysicalPersonRequestValidator()
    {
        RuleFor(x => x.PhysicalPersonId)
            .GreaterThan(0)
            .WithMessage(
                "PhysicalPersonId must be greater than 0.");

        RuleFor(x => x.PhoneNumber)
            .Must(x => x.ToString().Length == 11)
            .WithMessage(
                "PhoneNumber must be exactly 11 digits.");

        RuleFor(x => x.Ssno)
            .Must(x => x.ToString().Length == 7)
            .WithMessage(
                "SSNo must contain exactly 7 digits.");
    }
}