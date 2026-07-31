using System;
using NRedisStack;
using StackExchange.Redis;
using Abstractions.IRedisService;
using Services.Implementations.RedisI;
using Abstractions.IEmployeeRepo;
using Services.Implementations.EmployeeI;
using Abstractions.IFileService;
using Services.Implementations.FileServiceI;


namespace RedisConsoleAppv1 { 
    
    public class RedisConsoleAppv1 { 
        
        public static void Main(string[] args) { 
            
            Runner runner = new Runner();

            IRedisService redisService = new RedisService(
                new RedisConfig( // instead of injecting config we can create a 
                    // connectionmultiplexer at start and then inject it
                    // that's why above unsed namespaces
                    host: MyEnv.Redis.host,
                    port: MyEnv.Redis.port,
                    user: MyEnv.Redis.user,
                    password: MyEnv.Redis.password
            ));

            IEmployeeRepo employeeRepo = new EmployeeRepo(redisService);
            IFileService fileService = new FileService(MyEnv.FileService.pathToDataFolder);

            runner.Run(
                redisService,
                employeeRepo,
                fileService
            );
        } 
    } 
}