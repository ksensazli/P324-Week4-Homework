using AutoMapper;
using Bookstore.DTOs.BookDTOs;
using Bookstore.Operations.BookOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.BookValidations;
using FluentValidation.TestHelper;
using Moq;

namespace Bookstore.Tests.ValidatorTests.Book;

public class CreateBookCommandValidatorTests
{
    private readonly CreateBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper; 

    public CreateBookCommandValidatorTests()
    {
        _validator = new CreateBookCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateBookCommand(_mockMapper.Object, _mockUnitOfWork.Object)
        {
            Model = new BookCreateDto { GenreId = 0, PageCount = 100, PublishDate = DateTime.Now.AddDays(-1), Title = "Valid Title" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.GenreId);
    }

    [Fact]
    public void Validate_WhenPageCountIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateBookCommand(_mockMapper.Object, _mockUnitOfWork.Object)
        {
            Model = new BookCreateDto { GenreId = 1, PageCount = 0, PublishDate = DateTime.Now.AddDays(-1), Title = "Valid Title" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.PageCount);
    }

    [Fact]
    public void Validate_WhenPublishDateIsInTheFuture_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateBookCommand(null, null)
        {
            Model = new BookCreateDto 
            { 
                GenreId = 1, 
                PageCount = 100, 
                PublishDate = DateTime.Now.AddDays(1), // Future date
                Title = "Valid Title" 
            }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.PublishDate)
            .WithErrorMessage("Publish date must be in the past.");
    }

    [Fact]
    public void Validate_WhenTitleIsTooShort_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateBookCommand(_mockMapper.Object, _mockUnitOfWork.Object)
        {
            Model = new BookCreateDto { GenreId = 1, PageCount = 100, PublishDate = DateTime.Now.AddDays(-1), Title = "abc" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenAllPropertiesAreValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateBookCommand(_mockMapper.Object, _mockUnitOfWork.Object)
        {
            Model = new BookCreateDto { GenreId = 1, PageCount = 100, PublishDate = DateTime.Now.AddDays(-1), Title = "Valid Title" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}