using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRO.Core.ISignalR;
using PRO.Core.Models;
using PRO.Core.Services;
using PRO.Services.SignalR;

namespace PRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper, IHubContext<BroadcastHub, IHubClient> hubContext, INotificationService notificationService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        // GET: api/Employees  
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _employeeService.GetEmployee();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid().ToString();

            Notification notification = new Notification()
            {
                EmployeeName = employee.Name,
                TranType = "Add"
            };
            await _notificationService.AddNotification(notification);

            try
            {
                await _employeeService.AddEmployee(employee);
                await _hubContext.Clients.All.BroadcastMessage();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

    }
}