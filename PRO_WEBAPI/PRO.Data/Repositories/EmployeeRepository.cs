using PRO.Core.Models;
using PRO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Data.Repositories
{
   public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ProDbContext ProDbContext { get { return Context as ProDbContext; } }

        public EmployeeRepository(ProDbContext context) : base(context)
        {
        }
  
    }
}
