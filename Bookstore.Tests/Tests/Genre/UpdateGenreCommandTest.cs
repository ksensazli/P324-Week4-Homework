using Bookstore.DTOs.GenreDTOs;
using Bookstore.Operations.GenreOperations;
using Bookstore.UnitOfWork;
using Moq;

namespace Bookstore.Tests.Tests.Genre;

public class UpdateGenreCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateGenreCommand _command;

    public UpdateGenreCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new UpdateGenreCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenGenreDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdAsync(It.IsAny<int>()))!
            .ReturnsAsync((Entities.Genre)null!);

        _command.GenreId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenGenreExists_UpdatesGenreAndSavesChanges()
    {
        // Arrange
        var existingGenre = new Entities.Genre { Id = 1, Name = "Existing Genre", IsActive = true };
        _unitOfWorkMock.Setup(u => u.GenreRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingGenre);

        _command.GenreId = 1;
        _command.Model = new GenreUpdateDto() { Name = "Updated Genre", IsActive = false };

        // Act
        await _command.Handle();

        // Assert
        _unitOfWorkMock.Verify(u => u.GenreRepository.UpdateAsync(It.IsAny<Entities.Genre>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        Assert.Equal("Updated Genre", existingGenre.Name);
        Assert.False(existingGenre.IsActive);
    }
}