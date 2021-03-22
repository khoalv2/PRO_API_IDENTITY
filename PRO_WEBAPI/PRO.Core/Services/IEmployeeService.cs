using PRO.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core.Services
{
   public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployee();

        Task<Employee> GetEmployeeDetail(string id);
        void DeleteEmployee(Employee employee);

        Task AddEmployee(Employee employee);
    }
}
