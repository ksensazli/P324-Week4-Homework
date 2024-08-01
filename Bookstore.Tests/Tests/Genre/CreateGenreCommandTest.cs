using System.Linq.Expressions;
using AutoMapper;
using Bookstore.DTOs.GenreDTOs;
using Bookstore.Operations.GenreOperations;
using Bookstore.UnitOfWork;
using Moq;

namespace Bookstore.Tests.Tests.Genre;

public class CreateGenreCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateGenreCommand _command;

    public CreateGenreCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _command = new CreateGenreCommand(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenGenreAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Entities.Genre, bool>>>()))
            .ReturnsAsync(new Entities.Genre());

        _command.Model = new GenreCreateDto() { Name = "Existing Genre" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenGenreDoesNotExist_AddsGenreAndSavesChanges()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Entities.Genre, bool>>>()))!
            .ReturnsAsync((Entities.Genre)null!);

        var newGenre = new Entities.Genre { Id = 1, Name = "New Genre" };
        _mapperMock.Setup(m => m.Map<Entities.Genre>(It.IsAny<GenreCreateDto>())).Returns(newGenre);

        _command.Model = new GenreCreateDto { Name = "New Genre" };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.GenreRepository.AddAsync(It.IsAny<Entities.Genre>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}