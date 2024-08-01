using AutoMapper;
using Bookstore.DTOs.GenreDTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.GenreOperations;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetGenreDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<GenreDto> Handle()
    {
        var genre = await unitOfWork.GenreRepository.GetWhereAsync(x => x.Id == GenreId);

        if (genre is null)
        {
            throw new InvalidOperationException("There is no genre with this genre id.");
        }

        GenreDto viewModel = mapper.Map<GenreDto>(genre);
        return viewModel;
        
    }
}