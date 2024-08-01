using Bookstore.Entities;

namespace Bookstore.DTOs;

public class AuthorDto
{
    public string FullName { get; set; }
    public ICollection<Book> Books { get; set; }
}