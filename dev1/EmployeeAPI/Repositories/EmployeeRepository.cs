using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _collection;
    private readonly ILogger<Employee> _logger;

    public EmployeeRepository(
        IOptions<EmployeeDatabaseSettings> settings,
        ILogger<Employee> logger)
    {
        var client = new MongoClient(
            settings.Value.ConnectionString);

        var database = client.GetDatabase(
            settings.Value.DatabaseName);

        _collection = database.GetCollection<Employee>(
            settings.Value.EmployeeCollectionName);

        _logger = logger;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        try
        {
            var listEmployee = await _collection
                 .Find(_ => true)
                 .ToListAsync();

            return listEmployee;
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while getting the list of employees from MongoDB");
            throw;
        }
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        try
        {
            var employee = await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            return employee;
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while getting the data from MongoDB based on ID");
            throw;
        }
    }

    public async Task CreateAsync(Employee employee)
    {
        try
        {
            await _collection.InsertOneAsync(employee);
        }

        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while creating employee with emailId {emailId}", employee.Email);
            throw;
        }
    }

    public async Task UpdateAsync(string id, Employee employee)
    {
        try
        {
            await _collection.ReplaceOneAsync(
            x => x.Id == id,
            employee);
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while updating the employee");
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try
        {
            await _collection.DeleteOneAsync(
            x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while deleting the employee with EmployeeId: {empId}", id);
            throw;
        }
    }
}