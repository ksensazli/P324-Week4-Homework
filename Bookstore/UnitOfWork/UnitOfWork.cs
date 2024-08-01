using Bookstore.Context;
using Bookstore.Entities;
using Bookstore.Repository;

namespace Bookstore.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<Book> BookRepository { get; } 
    public IRepository<Genre> GenreRepository { get; } 
    public IRepository<Author> AuthorRepository { get; }
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        BookRepository = new Repository<Book>(this._context);
        GenreRepository = new Repository<Genre>(this._context);
        AuthorRepository = new Repository<Author>(this._context);
        
    }
    
    public async Task Dispose()
    {
        await _context.DisposeAsync();
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }
}