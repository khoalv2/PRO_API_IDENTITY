using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRO.API.Resources
{
    public class MusicResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistResource Artist { get; set; }
    }
}
