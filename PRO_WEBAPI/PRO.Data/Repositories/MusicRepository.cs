using Microsoft.EntityFrameworkCore;
using PRO.Core;
using PRO.Core.Filter;
using PRO.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private ProDbContext ProDbContext { get { return Context as ProDbContext; } }

        public MusicRepository(ProDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync(PaginationFilter filter)
        {
            return await ProDbContext.Musics.Include(x => x.Artist)
                 .Skip((filter.PageNumber - 1) * filter.PageSize)
                 .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await ProDbContext.Musics.Include(x => x.Artist).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await ProDbContext.Musics.Include(x => x.Artist).Where(x => x.ArtistId == artistId).ToListAsync();
        }
    }
}
