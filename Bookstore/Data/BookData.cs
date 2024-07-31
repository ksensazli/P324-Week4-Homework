using Bookstore.Model;

namespace Bookstore.Data;

public static class BookData
{
    public static List<Book> BookList = new List<Book>()
    {
        new Book
        {
            Id = 1,
            Title = "Book of 5 Rings",
            AuthorId = 2,
            GenreId = 5, // Philosophy
            PageCount = 128,
            PublishDate = new DateTime(1645, 1, 1),
        },
        new Book
        {
            Id = 2,
            Title = "Meditations",
            AuthorId = 3,
            GenreId = 5, // Philosophy
            PageCount = 112,
            PublishDate = new DateTime(54, 1, 1),
        },
        new Book
        {
            Id = 3,
            Title = "Dune",
            AuthorId = 1,
            GenreId = 2, // Science-Fiction
            PageCount = 879,
            PublishDate = new DateTime(2001, 1, 1),
        }
    };
}