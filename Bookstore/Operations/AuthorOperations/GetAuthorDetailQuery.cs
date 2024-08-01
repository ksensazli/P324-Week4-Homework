using AutoMapper;
using Bookstore.DTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.AuthorOperations;

public class GetAuthorDetailQuery
{
    public int AuthorId { get; set; }

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAuthorDetailQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<AuthorDto> Handle()
    {
        var author = await unitOfWork.AuthorRepository.GetWhereAsync(x => x.Id == AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException("There is no author with this author id.");
        }

        AuthorDto viewModel = mapper.Map<AuthorDto>(author);
        return viewModel;
    }
}