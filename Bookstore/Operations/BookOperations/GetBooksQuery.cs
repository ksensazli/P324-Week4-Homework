using AutoMapper;
using Bookstore.DTOs.BookDTOs;
using Bookstore.UnitOfWork;

namespace Bookstore.Operations.BookOperations;

public class GetBooksQuery
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetBooksQuery( IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<BookGetDto>> Handle()
    {
        var bookList = await unitOfWork.BookRepository.GetAllAsync();

        List<BookGetDto> books = mapper.Map<List<BookGetDto>>(bookList);

        return books;
    }
}