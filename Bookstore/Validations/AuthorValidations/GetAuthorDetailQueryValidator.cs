using Bookstore.Operations.AuthorOperations;
using FluentValidation;

namespace Bookstore.Validations.AuthorValidations;

public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be greater than zero.");
    }
}