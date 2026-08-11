namespace ddd_Employee_PhysicalPerson.Infra;

public interface IRedisRepository
{
    public bool SetJson(string key, object value);
    public T GetJson<T>(string key);
    public Task<bool> KeyExistsAsync(string key);
}