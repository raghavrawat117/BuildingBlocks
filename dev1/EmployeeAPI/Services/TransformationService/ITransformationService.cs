namespace EmployeeAPI.Services.TransformationService;

public interface ITransformationService
{
    Task<string> TransformEmployeeDataBasedOnTemplateAsync(string templateName, object empData);
}