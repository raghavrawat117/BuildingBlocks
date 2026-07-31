
using System.Text.Json;
using System.Text.Json.Serialization;
using Abstractions.IEmployeeRepo;
using Abstractions.IRedisService;
using Scehmas;

namespace Services.Implementations.EmployeeI {
    public class EmployeeRepo : IEmployeeRepo
    {
        // constructor is at the end
        private readonly IRedisService _redisService;

        public Employee GetEmployee(int empId)
        {
            try{
                string result = _redisService.GetJson($"emp:{empId.ToString()}");

                if ( String.IsNullOrEmpty(result) ) { return null; }

                Employee e = !String.IsNullOrEmpty(result) ? JsonSerializer.Deserialize<Employee>(result) : null;
                return e;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }

        public bool InsertEmployee(Employee employee)
        {
            try{
                bool result = _redisService.SetJson($"emp:{employee.EmpId.ToString()}", employee);
                return result;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool InsertEmployees(List<Employee> employees)
        {
            try{
                bool result = false;
                foreach( Employee e in employees){
                    result = _redisService.SetJson($"emp:{e.EmpId.ToString()}", e);
                }
                return result;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool UpdateEmployee(int empId, UpdateEmployee updateEmployee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployees(List<UpdateEmployee> updateEmployees)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int empId)
        {
            try{
                bool result = _redisService.DeleteString($"emp:{empId.ToString()}");
                return result;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public bool DeleteEmployees(List<int> empIds)
        {
            try{
                // https://share.google/aimode/jLYPIyJRA6AZLab6p
                // no need, excuse me.

                List<string> employeeKeys = new List<string>();
                foreach(int empId in empIds){
                    employeeKeys.Add($"emp:{empId}");
                }
                bool result = _redisService.DeleteStrings(employeeKeys);
                return result;
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        public EmployeeRepo(IRedisService redisService)
        {
            _redisService = redisService;
        }
    }
}
