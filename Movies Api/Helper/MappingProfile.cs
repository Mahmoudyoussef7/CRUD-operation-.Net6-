using AutoMapper;

namespace Movies_Api.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<MovieDetailsDto, Movie>();
            CreateMap<MovieDto, Movie>().ForMember(d => d.Poster, opt => opt.Ignore());
        }
    }
}
