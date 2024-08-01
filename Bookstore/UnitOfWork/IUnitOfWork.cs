using Bookstore.Entities;
using Bookstore.Repository;

namespace Bookstore.UnitOfWork;

public interface IUnitOfWork
{
    Task Dispose();
    Task Complete();
    public IRepository<Book> BookRepository { get; } 
    public IRepository<Genre> GenreRepository { get; } 
    public IRepository<Author> AuthorRepository { get; } 
}