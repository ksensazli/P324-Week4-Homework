using Bookstore.Operations.GenreOperations;
using FluentValidation;

namespace Bookstore.Validations.GenreValidations;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(query => query.GenreId)
            .GreaterThan(0).WithMessage("Genre ID must be greater than zero.");
    }
}