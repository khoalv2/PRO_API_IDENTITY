using PRO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        INotificatioinRepository Notifications { get; }
        IEmployeeRepository Employees { get; }
        Task<int> CommitAsync();
    }
}
