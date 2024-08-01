using Bookstore.Operations.BookOperations;
using FluentValidation;

namespace Bookstore.Validations.BookValidations;

public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}