using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Template_Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeAPI.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<TemplateModel> _collection;

    public TemplateRepository(
        IOptions<EmployeeDatabaseSettings> settings)
    {
        var client =
            new MongoClient(
                settings.Value.ConnectionString);

        var database =
            client.GetDatabase(
                settings.Value.DatabaseName);

        _collection =
            database.GetCollection<TemplateModel>(
                settings.Value.TemplateCollectionName);
    }

    public async Task<TemplateModel?> GetTemplateByNameAsync(
        string templateName)
    {
        return await _collection
            .Find(x => x.TemplateName == templateName)
            .FirstOrDefaultAsync();
    }
}