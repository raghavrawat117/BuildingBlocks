using StackExchange.Redis;
using Abstractions.IRedisService;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;

namespace Services.Implementations.RedisI
{
    public class RedisService : IRedisService
    {
        private readonly RedisConfig _redisConfig;
        private readonly IDatabase _db;
        private readonly IJsonCommands _jsonCommands;

        public RedisService(RedisConfig redisConfig)
        {
            _redisConfig = redisConfig;
            IConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints = { { _redisConfig.Host, _redisConfig.Port } },
                    User = _redisConfig.User,
                    Password = _redisConfig.Password
                }
            );
            _db = muxer.GetDatabase();
            _jsonCommands = _db.JSON();
        }

        public string IndexedSearch(string indexName, string query)
        {
            // https://redis.io/docs/latest/develop/clients/dotnet/nredisstack/queryjson/#query-the-data
            try
            {
                // $ means the root path of the JSON document
                SearchResult resultOfIndexedSearch = _db.FT().Search(
                    indexName, new Query(query)
                );
                // // for debugging purposes, Ctrl + K + C comment Ctrl + K + U uncomment
                Console.WriteLine($"{indexName}   {query}");

                if (resultOfIndexedSearch == null) { return ""; }
                Console.WriteLine(string.Join(", ", resultOfIndexedSearch.Documents.Select(x => x["json"])));

                string resultString = "[" +
                            string.Join(", ", resultOfIndexedSearch.Documents.Select(x => x["json"]))
                        + "]";

                // // for debugging purposes, Ctrl + K + C comment Ctrl + K + U uncomment
                // Console.WriteLine(resultString);
                return resultString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return "";
            }
        }

        public string GetString(string key)
        {
            try
            {
                RedisValue value = _db.StringGet(key);
                return value.HasValue ? value.ToString() : "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return "";
            }
            // it does exist in current context
            // return value;
        }

        public bool SetString(string key, string value)
        {
            try
            {
                _db.StringSet(key, value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public string GetJson(string key)
        {
            try
            {
                RedisResult result = _jsonCommands.Get(key);
                return result.IsNull ? string.Empty : result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return "";
            }
        }

        public bool SetJson(string key, object value)
        {
            try
            {
                // $ means the root path of the JSON document
                _jsonCommands.Set(key, "$", value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool KeyExists(string key)
        {
            try
            {
                return _db.KeyExists(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //JSON.Merge only Overwrites the fields present in "Value"
        //Note - Passing Null will delete that field
        public bool MergeJson(string key, string path, object value)
        {
            try
            {
                _jsonCommands.Merge(key,path,value);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool SetStrings(Dictionary<string, string> keyValuePairs)
        {
            try
            { // this method is MGET equivalent
                List<KeyValuePair<RedisKey, RedisValue>> redisKeyValuePairs
                = new List<KeyValuePair<RedisKey, RedisValue>>();

                foreach (KeyValuePair<string, string> kv in keyValuePairs)
                {
                    redisKeyValuePairs.Add(new KeyValuePair<RedisKey, RedisValue>(kv.Key, kv.Value));
                }
                _db.StringSet(redisKeyValuePairs.ToArray()); // needs to be an array
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool DeleteString(string key)
        {
            try
            {
                _db.KeyDelete(key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool DeleteStrings(List<string> keys)
        {
            try
            { // https://share.google/aimode/x36h6X7kXfHxpGeni
                RedisKey[] keysToDelete = keys.Select(k => (RedisKey)k).ToArray();
                _db.KeyDelete(keysToDelete);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool DeleteJson(string keys)
        {
            try
            {
                _jsonCommands.Del(keys);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public void futureMethod()
        {
            // you can write the code here 
            // keep the below except, code will compile ✅
            throw new NotImplementedException();
        }
    }
}
