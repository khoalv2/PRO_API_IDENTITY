using PRO.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRO.Core.Services
{
    public interface IMusicService
    {
        // Task<IEnumerable<Music>> GetAllWithArtist();
        Task<IEnumerable<Music>> GetAllWithArtist(PaginationFilter filter);
        Task<Music> GetMusicById(int id);
        Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
        Task<Music> CreateMusic(Music newMusic);
        Task UpdateMusic(Music musicToBeUpdated, Music music);
        Task DeleteMusic(Music music);
    }
}
