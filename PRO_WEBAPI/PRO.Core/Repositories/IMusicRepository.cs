using PRO.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core.Repositories
{
   public interface IMusicRepository : IRepository<Music>
    {
       // Task<IEnumerable<Music>> GetAllWithArtistAsync();
        Task<IEnumerable<Music>> GetAllWithArtistAsync(PaginationFilter filter);
        Task<Music> GetWithArtistByIdAsync(int id);
        Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId);
    }
}
