using PRO.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core.Services
{
  public  interface INotificationService
    {
        Task AddNotification(Notification notification);

        Task<int> GetNotificationCount();

        Task<IEnumerable<Notification>> GetNotificationMessage();
    }
}
