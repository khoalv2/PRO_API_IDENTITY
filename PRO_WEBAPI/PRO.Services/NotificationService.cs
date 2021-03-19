using PRO.Core;
using PRO.Core.Models;
using PRO.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task AddNotification(Notification notification)
        {
            await _unitOfWork.Notifications.AddAsync(notification);
        }
    }
}
