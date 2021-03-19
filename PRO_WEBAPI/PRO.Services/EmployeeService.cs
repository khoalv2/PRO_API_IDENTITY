using PRO.Core;
using PRO.Core.Models;
using PRO.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }



        public async Task AddEmployee(Employee employee)
        {
             await _unitOfWork.Employees.AddAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }
    }
}
