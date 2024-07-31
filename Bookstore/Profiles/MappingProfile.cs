using AutoMapper;
using Bookstore.DTOs;
using Bookstore.Entities;

namespace Bookstore.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorCreateDto, Author>();
        CreateMap<AuthorUpdateDto, Author>();
    }
}