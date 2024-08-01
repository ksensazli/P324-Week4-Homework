using Bookstore.Operations.AuthorOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.AuthorValidations;
using FluentValidation.TestHelper;
using Moq;

namespace Bookstore.Tests.Tests.Author;

public class DeleteAuthorCommandValidatorTest
{
    private readonly DeleteAuthorCommandValidator _validator;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    public DeleteAuthorCommandValidatorTest()
    {
        _validator = new DeleteAuthorCommandValidator();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void Validate_WhenAuthorIdIsZero_ShouldHaveValidationError()
    {
        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 0 };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void Validate_WhenAuthorIdIsGreaterThanZero_ShouldNotHaveValidationError()
    {
        var command = new DeleteAuthorCommand(_mockUnitOfWork.Object) { AuthorId = 1 };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.AuthorId);
    }
}