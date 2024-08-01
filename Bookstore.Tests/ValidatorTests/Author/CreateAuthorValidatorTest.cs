using AutoMapper;
using Bookstore.DTOs;
using Bookstore.Operations.AuthorOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Moq;

namespace Bookstore.Tests.ValidatorTests.Author;

public class CreateAuthorCommandValidatorTest
{
    private readonly CreateAuthorCommandValidator _validator;

    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public CreateAuthorCommandValidatorTest()
    {
        _validator = new CreateAuthorCommandValidator();
        _mockMapper = new Mock<IMapper>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenModelIsNull_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new AuthorCreateDto { FullName = "Valid Name" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Model);
    }

    [Fact]
    public void Validate_WhenAuthorNameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new AuthorCreateDto { FullName = string.Empty }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.FullName)
              .WithErrorMessage("Author name is required.");
    }

    [Fact]
    public void Validate_WhenAuthorNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new AuthorCreateDto { FullName = new string('a', 101) }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Model.FullName)
              .WithErrorMessage("Author name must not exceed 100 characters.");
    }

    [Fact]
    public void Validate_WhenAuthorNameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateAuthorCommand(_mockUnitOfWork.Object, _mockMapper.Object)
        {
            Model = new AuthorCreateDto { FullName = "Valid Author" }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Model.FullName);
    }
}