using System;
using System.Text.Json;
using Abstractions.IEmployeeRepo;
using Abstractions.IFileService;
using Abstractions.IRedisService;
using Scehmas;

namespace RedisConsoleAppv1 { 
    
    public class Runner { 
        
        public void Run(
            IRedisService redisService,
            IEmployeeRepo employeeRepo,
            IFileService fileService
            ) 
        { 
            // ctrl K C | comment
            // ctrl K U | uncomment
            string ans = "";
            bool result = false;

            // Index Search of Redis
            ans = redisService.IndexedSearch("empIdx:v2", "@experience:[1,10]");

            // Inserting one employee
            // Employee employee = new Employee{
            //     EmpId = 5,
            //     FirstName = "John",
            //     LastName = "Doe",
            //     Location = "New York",
            //     Grade = Grade.MidLevel,
            //     Experience = 5,
            //     PhoneNumber = 1234567890,
            //     WorkEmail = "john.doe@laminar.com"
            // };
           // result = employeeRepo.InsertEmployee(employee);
           // ans = redisService.GetString("Emp");

            // // Check the get employee
            // Employee e_ans = employeeRepo.GetEmployee(5);
            // Console.Write(JsonSerializer.Serialize(e_ans));

            //Inserting many = Load
            // List<Employee>? employees = JsonSerializer
            //                             .Deserialize<List<Employee>>
            //                             (fileService.FileToString("employees.json"));
            // result = employeeRepo.InsertEmployees(employees);
            
            Console.WriteLine(result);
             Console.WriteLine(ans);
        } 
    } 
}