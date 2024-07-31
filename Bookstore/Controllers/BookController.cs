using Bookstore.Data;
using Bookstore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
	[HttpGet]
	public List<Book> GetBooks()
	{
		return BookData.BookList.OrderBy(x => x.Id).ToList<Book>();
	}

	[HttpGet("{id}")]
	public Book GetById(int id)
	{
		return BookData.BookList.Where(book => book.Id == id).SingleOrDefault();
	}

	[HttpPost]
	public IActionResult AddBook([FromBody] Book newBook)
	{
		if ((BookData.BookList.Find(x => x.Title == newBook.Title)) is not null)
		{
			return BadRequest();
		}
		BookData.BookList.Add(newBook);
		return Ok();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
	{
		var book = BookData.BookList.Find(x => x.Id == id);
		if (book is null)
		{
			return BadRequest();
		}

		book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
		book.AuthorId = updatedBook.AuthorId != default ? updatedBook.AuthorId : book.AuthorId;
		book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
		book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
		book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteBook(int id)
	{
		var book = BookData.BookList.Find(x => x.Id == id);

		if (book is null)
		{
			return BadRequest();
		}

		BookData.BookList.Remove(book);

		return Ok();
	}
}