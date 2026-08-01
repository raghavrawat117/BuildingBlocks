
using System.ComponentModel.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using Abstractions.IEmployeeRepo;
using Abstractions.IRedisService;
using Scehmas;

namespace Services.Implementations.EmployeeI
{
    public class EmployeeRepo : IEmployeeRepo
    {
        // constructor is at the end
        private readonly IRedisService _redisService;

        public Employee GetEmployee(int empId)
        {
            try
            {
                string result = _redisService.GetJson($"emp:{empId.ToString()}");

                if (String.IsNullOrEmpty(result)) { return null; }

                Employee e = !String.IsNullOrEmpty(result) ? JsonSerializer.Deserialize<Employee>(result) : null;
                return e;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }

        public bool InsertEmployee(Employee employee)
        {
            try
            {
                bool result = _redisService.SetJson($"emp:{employee.EmpId.ToString()}", employee);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool InsertEmployees(List<Employee> employees)
        {
            try
            {
                bool result = false;
                foreach (Employee e in employees)
                {
                    result = _redisService.SetJson($"emp:{e.EmpId.ToString()}", e);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool UpdateEmployee(UpdateEmployee updateEmployee)
        {
            try
            {
                string key = $"emp:{updateEmployee.EmpId}";
                if (!_redisService.KeyExists(key))
                    return false;

                var patch = BuildPatch(updateEmployee);
                if (patch.Count == 0)
                    return false;
                return _redisService.MergeJson(key, "$", patch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateEmployees(List<UpdateEmployee> updateEmployees)
        {
            try
            {
                bool allSucceded = true;
                foreach (var updateEmployee in updateEmployees)
                {
                    string key = $"emp:{updateEmployee.EmpId}";
                    //Check if employee exists
                    if (!_redisService.KeyExists(key))
                    {
                        allSucceded = false;
                        continue;
                    }
                    //build the patch for change fields in updateEmployee
                    var patch = BuildPatch(updateEmployee);
                    if (patch.Count == 0)
                        continue;
                    
                    bool success = _redisService.MergeJson(key, "$", patch);
                    if (!success)
                        allSucceded = false;

                }
                return allSucceded;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                bool result = _redisService.DeleteString($"emp:{empId.ToString()}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool DeleteEmployees(List<int> empIds)
        {
            try
            {
                // https://share.google/aimode/jLYPIyJRA6AZLab6p
                // no need, excuse me.

                List<string> employeeKeys = new List<string>();
                foreach (int empId in empIds)
                {
                    employeeKeys.Add($"emp:{empId}");
                }
                bool result = _redisService.DeleteStrings(employeeKeys);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        //Build a partial-update payload for RedisJSON's JSON.Merge commane
        private Dictionary<string, object> BuildPatch(UpdateEmployee updateEmployee)
        {
            var patch = new Dictionary<string, object>();
            if (updateEmployee.FirstName is not null) patch["firstName"] = updateEmployee.FirstName;
            if (updateEmployee.LastName is not null) patch["lastName"] = updateEmployee.LastName;
            if (updateEmployee.Location is not null) patch["location"] = updateEmployee.Location;
            if (updateEmployee.Grade is not null) patch["grade"] = updateEmployee.Grade;
            if (updateEmployee.Experience is not null) patch["experience"] = updateEmployee.Experience;
            if (updateEmployee.PhoneNumber is not null) patch["phoneNumber"] = updateEmployee.PhoneNumber;
            if (updateEmployee.WorkEmail is not null) patch["workEmail"] = updateEmployee.WorkEmail;

            return patch;

        }
        public EmployeeRepo(IRedisService redisService)
        {
            _redisService = redisService;
        }
    }
}
