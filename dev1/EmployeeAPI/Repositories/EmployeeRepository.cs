using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeAPI.Repositories;

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
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "Error occured while getting the list of employees from MongoDB");
            throw new DatabaseConnectionException("Failed to connect to the database. Please try again later.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "MongoDB operation timed out while getting the list of employees");
            throw new DatabaseConnectionException("Database operation timed out. Please try again later.", ex);
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
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "Error occured while getting the data from MongoDB based on ID");
            throw new DatabaseConnectionException("Failed to connect to the database. Please try again later.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "MongoDB operation timed out while getting the data from MongoDB based on ID");
            throw new DatabaseConnectionException("Database operation timed out. Please try again later.", ex);
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while getting the data from MongoDB based on ID");
            throw;
        }
        catch (Exception ex)
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
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "Error occured while creating employee with emailId {emailId}", employee.Email);
            throw new DatabaseConnectionException("Failed to connect to the database. Please try again later.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "MongoDB operation timed out while creating employee with emailId {emailId}", employee.Email);
            throw new DatabaseConnectionException("Database operation timed out. Please try again later.", ex);
        }

        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while creating employee with emailId {emailId}", employee.Email);
            throw;
        }
        catch (Exception ex)
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
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "Error occured while updating the employee with EmployeeId: {empId}", id);
            throw new DatabaseConnectionException("Failed to connect to the database. Please try again later.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "MongoDB operation timed out while updating the employee with EmployeeId: {empId}", id);
            throw new DatabaseConnectionException("Database operation timed out. Please try again later.", ex);
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while updating the employee with EmployeeId: {empId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while updating the employee with EmployeeId: {empId}", id);
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
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "Error occured while deleting the employee with EmployeeId: {empId}", id);
            throw new DatabaseConnectionException("Failed to connect to the database. Please try again later.", ex);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "MongoDB operation timed out while deleting the employee with EmployeeId: {empId}", id);
            throw new DatabaseConnectionException("Database operation timed out. Please try again later.", ex);
        }
        catch (MongoException ex)
        {
            _logger.LogError(ex, "Error occured while deleting the employee with EmployeeId: {empId}", id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while deleting the employee with EmployeeId: {empId}", id);
            throw;
        }
    }

    public async Task<bool> EmployeeExistsAsync(string email)
    {
        return await _collection
            .Find(x => x.Email == email)
            .AnyAsync();
    }
}