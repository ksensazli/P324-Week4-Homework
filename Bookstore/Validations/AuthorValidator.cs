using Bookstore.DTOs;
using FluentValidation;

namespace Bookstore.Validations;

public class AuthorCreateValidator : AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateValidator()
    {
        RuleFor(author => author.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(author => author.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(author => author.BirthDate).NotEmpty().WithMessage("Birth date is required.");
    }
}

public class AuthorUpdateValidator : AbstractValidator<AuthorUpdateDto>
{
    public AuthorUpdateValidator()
    {
        RuleFor(author => author.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(author => author.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(author => author.BirthDate).NotEmpty().WithMessage("Birth date is required.");
    }
}