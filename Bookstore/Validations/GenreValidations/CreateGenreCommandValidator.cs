using Bookstore.Operations.GenreOperations;
using FluentValidation;

namespace Bookstore.Validations.GenreValidations;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .MinimumLength(2).WithMessage("Genre name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Genre name must not exceed 50 characters.");
    }
}