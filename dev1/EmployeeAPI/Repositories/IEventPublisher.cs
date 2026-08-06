using EmployeeAPI.Repositories;

namespace EmployeeAPI.Repositories;
public interface IEventPublisher
{
    Task PublishCreatedEmployeeAsync(object empData);
}