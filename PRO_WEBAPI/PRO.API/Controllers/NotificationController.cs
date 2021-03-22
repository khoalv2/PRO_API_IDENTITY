using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRO.API.Models;
using PRO.Core.Models;
using PRO.Core.Services;

namespace PRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController( IMapper mapper, INotificationService notificationService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        // GET: api/Notifications/notificationcount  
        [Route("notificationcount")]
        [HttpGet]
        public async Task<int> GetNotificationCount()
        {
            return await _notificationService.GetNotificationCount();
        }

        // GET: api/Notifications/notificationresult  
        [Route("notificationresult")]
        [HttpGet]
        public async Task<IEnumerable<NotificationResult>> GetNotificationMessage()
        {
            var results = await _notificationService.GetNotificationMessage();

            var reval = _mapper.Map<IEnumerable<Notification>,IEnumerable<NotificationResult>>(results);

            return reval;
        }

    }
}