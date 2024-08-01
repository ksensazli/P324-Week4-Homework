using Bookstore.Operations.GenreOperations;
using FluentValidation;

namespace Bookstore.Validations.GenreValidations;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.GenreId)
            .GreaterThan(0).WithMessage("Genre ID must be greater than zero.");

        RuleFor(command => command.Model)
            .NotNull().WithMessage("GenreUpdateDto model is required.");

        RuleFor(command => command.Model.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(50).WithMessage("Genre name must not exceed 50 characters.");

        RuleFor(command => command.Model.IsActive)
            .NotNull().WithMessage("IsActive field is required.");
    }
}