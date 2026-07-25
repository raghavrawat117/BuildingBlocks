namespace Abstractions.IRedisService { 
    
    public interface IRedisService
    {
        public string GetString(string key);
        public bool SetString(string key, string value);
        public bool SetStrings(Dictionary<string, string> keyValuePairs);
        public string GetJson(string key);
        public bool SetJson(string key, object value);
        public string IndexedSearch(string indexName, string query);
        public bool DeleteString(string key);
        public bool DeleteStrings(List<string> keys);
        public bool DeleteJson(string key);
        public void futureMethod();
    } 
}