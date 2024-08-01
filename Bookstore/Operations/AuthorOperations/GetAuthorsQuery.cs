using AutoMapper;
using Bookstore.DTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.AuthorOperations;

public class GetAuthorsQuery
{
    
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAuthorsQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<AuthorGetDto>> Handle()
    {
        var authorList = await unitOfWork.AuthorRepository.GetAllAsync();
        List<AuthorGetDto> authors = mapper.Map<List<AuthorGetDto>>(authorList);
        return authors;
        
    }
}