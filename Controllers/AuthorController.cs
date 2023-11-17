using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly BlogDbContext _context;

    private readonly ILogger<AuthorController> _logger;

    public AuthorController(ILogger<AuthorController> logger, BlogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("/author")]
    public IActionResult CreateAuthor([FromBody] AuthorRequestDto authorDto)
    {
        try
        {
            // TODO: Add validation (name, surname char limits)
            var newAuthor = new Author(authorDto.Name, authorDto.Surname);

            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            _logger.LogInformation("Author created successfully");
            return Ok($"Post with id {newAuthor.Id} created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating author");
            return StatusCode(500, "Internal Server Error");
        }

    }

    [HttpGet("/author/{id}")]
    public IActionResult GetAuthor(int id)
    {
        try
        {
            var author = _context.Authors.Find(id);

            if (author == null)
                return NotFound();

            // TODO: Include posts/number of posts
            var authorDto = new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname
            };   

            _logger.LogInformation("Author returned successfully");
            return Ok(authorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting author");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
