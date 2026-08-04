using EmployeeAPI.DTOs;

namespace EmployeeAPI.Services;
public interface IEventPublisher
{
    Task PublishCreatedEmployeeAsync(object empData);
}