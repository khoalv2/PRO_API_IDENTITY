using PRO.Core.Models;
using PRO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Data.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificatioinRepository
    {
        private ProDbContext ProDbContext { get { return Context as ProDbContext; } }

        public NotificationRepository(ProDbContext context) : base(context)
        {
        }
    }
}
