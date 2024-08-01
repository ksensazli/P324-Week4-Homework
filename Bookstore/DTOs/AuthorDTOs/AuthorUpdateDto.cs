using Bookstore.Entities;

namespace Bookstore.DTOs;

public class AuthorUpdateDto
{
    public string FullName { get; set; }
    public ICollection<Book> Books { get; set; }
}