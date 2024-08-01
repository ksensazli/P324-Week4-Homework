using Bookstore.DTOs;
using Bookstore.Operations.AuthorOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Moq;

namespace Bookstore.Tests.ValidatorTests.Author;

public class UpdateAuthorCommandValidatorTests
{
    private readonly UpdateAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public UpdateAuthorCommandValidatorTests()
    {
        _validator = new UpdateAuthorCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsZero_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 0 };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenAuthorIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 1 };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenNameIsNullOrEmpty_ShouldHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { Model = new AuthorUpdateDto { FullName = "" } };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Model.FullName);
    }

    [Fact]
    public void Validate_WhenNameIsNotNullOrEmpty_ShouldNotHaveValidationError()
    {
        var command = new UpdateAuthorCommand(_mockUnitOfWork.Object) { Model = new AuthorUpdateDto { FullName = "Valid Name" } };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Model.FullName);
    }
}