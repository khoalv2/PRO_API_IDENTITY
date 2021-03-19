using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRO.API.Models;
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

        //// GET: api/Notifications/notificationcount  
        //[Route("notificationcount")]
        //[HttpGet]
        //public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
        //{
        //    var count = (from not in _context.Notification
        //                 select not).CountAsync();
        //    NotificationCountResult result = new NotificationCountResult
        //    {
        //        Count = await count
        //    };
        //    return result;
        //}

        //// GET: api/Notifications/notificationresult  
        //[Route("notificationresult")]
        //[HttpGet]
        //public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()
        //{
        //    var results = from message in _context.Notification
        //                  orderby message.Id descending
        //                  select new NotificationResult
        //                  {
        //                      EmployeeName = message.EmployeeName,
        //                      TranType = message.TranType
        //                  };
        //    return await results.ToListAsync();
        //}

    }
}