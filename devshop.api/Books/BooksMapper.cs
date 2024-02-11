using AutoMapper;

namespace devshop.api.Books;

public sealed class BooksMapper : Profile
{
    public BooksMapper()
    {
        CreateMap<Book, BooksResponse>()
            .ForMember(dest => dest.PublishedAt, 
                opt => opt.MapFrom(x => x.HumanizedPublishedDate()));
        
        CreateMap<BooksCreateRequest, Book>()
            .ForMember(dest => dest.PublishedAt, 
                opt => opt.MapFrom(x => DateOnly.FromDateTime(x.PublishedAt)));

        CreateMap<BooksUpdateRequest, Book>()
            .ForMember(dest => dest.PublishedAt, opt => opt.Ignore());
    }
}