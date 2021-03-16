using Microsoft.EntityFrameworkCore;
using PRO.Core;
using PRO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(ProDbContext context)
                    : base(context)
        { }

        private ProDbContext ProDbContext { get { return Context as ProDbContext; } }

        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await ProDbContext.Artists.Include(a => a.Musics).ToListAsync();
        }

        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return await ProDbContext.Artists
                 .Include(a => a.Musics)
                 .SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
