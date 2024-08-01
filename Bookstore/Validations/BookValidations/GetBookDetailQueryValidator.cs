using Bookstore.Operations.BookOperations;
using FluentValidation;

namespace Bookstore.Validations.BookValidations;

public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}