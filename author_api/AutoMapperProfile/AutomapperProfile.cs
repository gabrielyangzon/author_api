
using author_data_types.Models;
using AutoMapper;

namespace author_api.AutoMapperProfile
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            ///Author
            CreateMap<AuthorCreateDto, Author>()
              .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
              .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName));

            CreateMap<Author, AuthorOnlyResponse>();
            //.ForMember(des => des.Id, opts => opts.MapFrom(src => src.Id))
            //.ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName));


            CreateMap<AuthorEditDto, Author>();
            ///Book
            CreateMap<BookCreateDto, Book>();

        }
    }
}
