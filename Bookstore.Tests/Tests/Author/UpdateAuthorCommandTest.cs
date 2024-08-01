using Bookstore.DTOs;
using Bookstore.Operations.AuthorOperations;
using Bookstore.UnitOfWork;
using Moq;

namespace Bookstore.Tests.Tests.Author;

public class UpdateAuthorCommandTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateAuthorCommand _command;

    public UpdateAuthorCommandTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _command = new UpdateAuthorCommand(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.AuthorRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Entities.Author)null);

        _command.AuthorId = 1;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _command.Handle());
    }

    [Fact]
    public async Task Handle_WhenAuthorExists_UpdatesAuthorAndSavesChanges()
    {
        // Arrange
        var existingAuthor = new Entities.Author { Id = 1, Name = "Old Name" };
        _unitOfWorkMock.Setup(u => u.AuthorRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingAuthor);

        _command.AuthorId = 1;
        _command.Model = new AuthorUpdateDto() { FullName = "New Name", Books = existingAuthor.Books };

        // Act
        await _command.Handle();

        // Assert
        Assert.Equal("New Name", existingAuthor.Name);
        _unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(existingAuthor), Times.Once);
        _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
    }
}