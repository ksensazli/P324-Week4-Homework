using Bookstore.Operations.GenreOperations;
using FluentValidation;

namespace Bookstore.Validations.GenreValidations;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.GenreId)
            .GreaterThan(0).WithMessage("Genre ID must be greater than zero.");
    }
}