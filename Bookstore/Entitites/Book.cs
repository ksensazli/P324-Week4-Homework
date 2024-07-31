namespace Bookstore.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; } // Author ile ilişkilendirme
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public Author Author { get; set; } // Author navigation property
}