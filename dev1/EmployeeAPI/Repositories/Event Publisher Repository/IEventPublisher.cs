namespace EmployeeAPI.Repositories.Event_Publisher_Repository;
public interface IEventPublisher
{
    Task PublishCreatedEmployeeAsync(object empData);
}