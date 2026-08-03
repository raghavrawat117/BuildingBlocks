using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public EmployeeService(
        IOptions<EmployeeDatabaseSettings> settings)
    {
        var mongoClient =
            new MongoClient(settings.Value.ConnectionString);

        var mongoDatabase =
            mongoClient.GetDatabase(
                settings.Value.DatabaseName);

        _employeeCollection =
            mongoDatabase.GetCollection<Employee>(
                settings.Value.EmployeeCollectionName);
    }

    public async Task<List<Employee>> GetAsync()
    {
        return await _employeeCollection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        return await _employeeCollection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Employee employee)
    {
        await _employeeCollection.InsertOneAsync(employee);
    }
}