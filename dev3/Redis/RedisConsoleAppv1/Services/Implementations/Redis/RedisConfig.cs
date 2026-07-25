using System; 

namespace Services.Implementations.RedisI { 
    
    public class RedisConfig
    {
        public RedisConfig(string host, int port, string user, string password)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    } 
}