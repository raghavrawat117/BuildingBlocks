using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _collection;

    public EmployeeRepository(
        IOptions<EmployeeDatabaseSettings> settings)
    {
        var client = new MongoClient(
            settings.Value.ConnectionString);

        var database = client.GetDatabase(
            settings.Value.DatabaseName);

        _collection = database.GetCollection<Employee>(
            settings.Value.EmployeeCollectionName);
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Employee employee)
    {
        await _collection.InsertOneAsync(employee);
    }
}