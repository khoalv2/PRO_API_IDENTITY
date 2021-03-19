using PRO.Core;
using PRO.Core.Repositories;
using PRO.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMusicRepository _musicRepository;

        public IArtistRepository _artistRepository;
        public IEmployeeRepository _employeeRepository;
        public INotificatioinRepository _notificatioins;
        private readonly ProDbContext _context;

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public IEmployeeRepository Employees => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);
        public INotificatioinRepository Notifications => _notificatioins = _notificatioins ?? new NotificationRepository(_context);


        public UnitOfWork(ProDbContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
