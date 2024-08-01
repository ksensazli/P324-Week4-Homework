using AutoMapper;
using Bookstore.DTOs.GenreDTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.GenreOperations;

public class GetGenresQuery
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetGenresQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<GenreGetDto>> Handle()
    {
        var genreList = await unitOfWork.GenreRepository.GetAllAsync();
        List<GenreGetDto> genres = mapper.Map<List<GenreGetDto>>(genreList);
        return genres;
    }
    
}