using AutoMapper;
using Bookstore.DTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.AuthorOperations;

public class CreateAuthorCommand
{
    public AuthorCreateDto Model { get; set; }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAuthorCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var author = await _unitOfWork.AuthorRepository.FirstOrDefaultAsync(x => x.Name == Model.FullName);
        if (author is not null)
        {
            throw new InvalidOperationException("This author already exists!");
        }

        await _unitOfWork.AuthorRepository.AddAsync(_mapper.Map<Entities.Author>(Model));
        await  _unitOfWork.Complete();
    }
}