using AutoMapper;
using PRO.API.Resources;
using PRO.Core;
using PRO.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRO.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Music, MusicResource>();
            CreateMap<Artist, ArtistResource>();

            // Resource to Domain
            CreateMap<MusicResource,Music>();
            CreateMap<ArtistResource, Artist>();

            CreateMap<SaveArtistResource, Artist>();
            CreateMap<SaveMusicResource, Music>();

            CreateMap<UserSignUpResource, User>().ForMember(x => x.UserName,x => x.MapFrom(a => a.Email));

        }
    }
}
