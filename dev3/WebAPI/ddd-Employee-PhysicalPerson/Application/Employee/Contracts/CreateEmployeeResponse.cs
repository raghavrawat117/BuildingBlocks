using ddd_Employee_PhysicalPerson.Application.Employee.BusinessRules;
using ddd_Employee_PhysicalPerson.Application.Employee.Validations;

namespace ddd_Employee_PhysicalPerson.Application.Employee.Contracts;

// public class CreateEmployeeResponse (int employeeId)
// {
//     public string AcknowledgementMessage => $"Employee {employeeId} created successfully";  
// }
public class CreateEmployeeResponse
{
    public string StatusMessage { get; }
    public CreateEmployeeResponse(string statusMessage)
    {
        StatusMessage = statusMessage;
    }

    // public CreateEmployeeResponse(ValidationException ex)
    // {
    //     AcknowledgementMessage = $"Validation has failed with erros:{string.Join(" , ", ex.Errors)}";
    // }
    // // // This is causing coupling :/
    // public CreateEmployeeResponse(CreateEmployeeValidationException ex)
    // {
    //     AcknowledgementMessage = $"Validation for PhysicalPerson with Id:{ex.FailedId} has failed with erros:{ex.ErrorMessage}";
    // }

    // public CreateEmployeeResponse(PhysicalPersonNotFoundException ex)
    // {
    //     AcknowledgementMessage = ex.ErrorMessage;
    // }    
}

// // Scalable way ---------------
/*
public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }
}
*/