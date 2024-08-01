using AutoMapper;
using Bookstore.DTOs.GenreDTOs;
using Bookstore.Operations.GenreOperations;
using Bookstore.UnitOfWork;
using Bookstore.Validations.GenreValidations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetGenresQuery(_unitOfWork, _mapper);
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetGenreDetailQuery(_unitOfWork, _mapper) { GenreId = id };

        var validator = new GetGenreDetailQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto newGenre)
    {
        var command = new CreateGenreCommand(_unitOfWork, _mapper) { Model = newGenre };

        var validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok($"{newGenre.Name} has saved successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto updatedGenre)
    {
        var command = new UpdateGenreCommand(_unitOfWork) { GenreId = id, Model = updatedGenre };

        var validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok($"{updatedGenre.Name} updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var command = new DeleteGenreCommand(_unitOfWork) { GenreId = id };

        var validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok("Genre deleted successfully.");
    }
}