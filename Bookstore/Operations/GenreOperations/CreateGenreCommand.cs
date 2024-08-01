using AutoMapper;
using Bookstore.DTOs.GenreDTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.GenreOperations;

public class CreateGenreCommand
{
    public GenreCreateDto Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGenreCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(x => x.Name == Model.Name);
        if (genre is not null)
        {
            throw new InvalidOperationException("This genre already exists!");
        }

        await _unitOfWork.GenreRepository.AddAsync(_mapper.Map<Entities.Genre>(Model));
        await  _unitOfWork.Complete();
    }
}