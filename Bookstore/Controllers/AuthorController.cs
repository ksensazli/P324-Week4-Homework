using AutoMapper;
using Bookstore.Data;
using Bookstore.DTOs;
using Bookstore.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private static List<Author> AuthorList = new List<Author>()
    {
        new Author { Id = 1, FirstName = "Frank", LastName = "Herbert", BirthDate = new DateTime(1920, 10, 8) },
        new Author { Id = 2, FirstName = "Miyamoto", LastName = "Musashi", BirthDate = new DateTime(1584, 3, 12) },
        new Author { Id = 3, FirstName = "Marcus", LastName = "Aurelius", BirthDate = new DateTime(121, 4, 26) }
    };

    private readonly IMapper _mapper;

    public AuthorController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _mapper.Map<List<AuthorDto>>(AuthorList);
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        var author = AuthorList.SingleOrDefault(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }
        var authorDto = _mapper.Map<AuthorDto>(author);
        return Ok(authorDto);
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] AuthorCreateDto newAuthor)
    {
        var author = _mapper.Map<Author>(newAuthor);
        author.Id = AuthorList.Max(a => a.Id) + 1;
        AuthorList.Add(author);
        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] AuthorUpdateDto updatedAuthor)
    {
        var author = AuthorList.SingleOrDefault(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        _mapper.Map(updatedAuthor, author);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = AuthorList.SingleOrDefault(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        var hasBooks = BookData.BookList.Any(b => b.AuthorId == id);
        if (hasBooks)
        {
            return BadRequest("Kitabı yayında olan bir yazar silinemez.");
        }

        AuthorList.Remove(author);
        return NoContent();
    }
}