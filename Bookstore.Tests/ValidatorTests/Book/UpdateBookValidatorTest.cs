using Bookstore.DTOs.BookDTOs;
using Bookstore.Operations.BookOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.BookValidations;
using FluentValidation.TestHelper;
using Moq;

namespace Bookstore.Tests.ValidatorTests.Book;

public class UpdateBookCommandValidatorTest
{
    private readonly UpdateBookCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateBookCommandValidatorTest()
    {
        _validator = new UpdateBookCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenBookIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { BookId = 1 , Model = new BookUpdateDto{ Title = "Valid Title" , GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenBookIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { BookId = 1 , Model = new BookUpdateDto{ Title = "Valid Title" , GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.BookId);
    }

    [Fact]
    public void Validate_WhenTitleIsNullOrEmpty_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new BookUpdateDto { Title = "" } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenTitleIsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new BookUpdateDto { Title = "Valid Title" } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.Title);
    }

    [Fact]
    public void Validate_WhenGenreIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new BookUpdateDto { GenreId = 0 } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.GenreId);
    }

    [Fact]
    public void Validate_WhenGenreIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateBookCommand(_mockUnitOfWork.Object) { Model = new BookUpdateDto { GenreId = 1 } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.GenreId);
    }
}