using Bookstore.Operations.AuthorOperations;
using FluentValidation;

namespace Bookstore.Validations.AuthorValidations;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model)
            .NotNull().WithMessage("CreateAuthorDTO model is required.");

        RuleFor(command => command.Model.FullName)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");


    }
}