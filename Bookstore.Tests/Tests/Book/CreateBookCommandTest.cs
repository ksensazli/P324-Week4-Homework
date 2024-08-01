using System.Linq.Expressions;
using AutoMapper;
using Bookstore.DTOs.BookDTOs;
using Bookstore.Operations.BookOperations;
using Bookstore.UnitOfWork;
using Moq;

namespace Bookstore.Tests.Tests.Book;

public class CreateBookCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateBookCommand _command;

    public CreateBookCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _command = new CreateBookCommand(_mapperMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenBookAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.BookRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Entities.Book, bool>>>()))
            .ReturnsAsync(new Entities.Book());

        _command.Model = new BookCreateDto() { Title = "Existing Book" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenBookDoesNotExist_AddsBookAndSavesChanges()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.BookRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Entities.Book, bool>>>()))
            .ReturnsAsync((Entities.Book)null);

        var newBook = new Entities.Book { Id = 1, Title = "New Book" };
        _mapperMock.Setup(m => m.Map<Entities.Book>(It.IsAny<BookCreateDto>())).Returns(newBook);

        _command.Model = new BookCreateDto { Title = "New Book", GenreId = 1, PageCount = 100, PublishDate = DateTime.Now };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.BookRepository.AddAsync(It.IsAny<Entities.Book>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}