using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories.Template_Repository
{
    public interface ITemplateRepository
    {
        Task<TemplateModel?> GetTemplateByNameAsync(string templateName);
    }
     
}
