using ddd_Employee_PhysicalPerson.Application.Employee.Validations;

namespace ddd_Employee_PhysicalPerson.Application.Employee.Contracts;

// public class CreateEmployeeResponse (int employeeId)
// {
//     public string AcknowledgementMessage => $"Employee {employeeId} created successfully";  
// }
public class CreateEmployeeResponse
{
    public CreateEmployeeResponse(int employeeId)
    {
        AcknowledgementMessage = $"Employee {employeeId} created successfully";
    }

    // public CreateEmployeeResponse(ValidationException ex)
    // {
    //     AcknowledgementMessage = $"Validation has failed with erros:{string.Join(" , ", ex.Errors)}";
    // }

    public CreateEmployeeResponse(CreateEmployeeValidationException ex)
    {
        AcknowledgementMessage = $"Validation for PhysicalPerson with Id:{ex.FailedId} has failed with erros:{ex.ErrorMessage}";
    }

    public string AcknowledgementMessage { get; }


}