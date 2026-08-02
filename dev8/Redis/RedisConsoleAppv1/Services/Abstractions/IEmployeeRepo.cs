using Scehmas;
using System.Collections.Generic;
namespace Abstractions.IEmployeeRepo { 
    
    public interface IEmployeeRepo
    {
        public Employee GetEmployee(int empId);
        public bool InsertEmployee(Employee employee);
        public bool InsertEmployees(List<Employee> employees);
        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="empId">The ID of the employee to update.</param>
        /// <param name="updateEmployee">The updated employee information.</param>
        /// <returns>True if the employee was updated successfully, false otherwise.</returns>
         public bool UpdateEmployee(UpdateEmployee updateEmployee);
        // //TBD
        public List<Employee> GetEmployeeByLocation(string location);
        // //TBD
        public bool UpdateEmployees(List<UpdateEmployee> updateEmployees);
        public bool DeleteEmployee(int empId);
        public bool DeleteEmployees(List<int> empIds);

    } 
}