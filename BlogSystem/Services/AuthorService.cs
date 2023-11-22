using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Services;

public class AuthorService : IAuthorService
{
    private readonly BlogDbContext _context;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(BlogDbContext context, ILogger<AuthorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> CreateAuthorAsync(AuthorRequestDto authorDto)
    {
        try
        {
            var newAuthor = new Author(authorDto.Name, authorDto.Surname);

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Author created successfully");
            return new OkObjectResult(new AuthorResponseDto(newAuthor.Id, newAuthor.Name, newAuthor.Surname));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating author");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> GetAuthorAsync(int id)
    {
        try
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return new NotFoundResult();

            var authorDto = new AuthorResponseDto(author.Id, author.Name, author.Surname);

            _logger.LogInformation("Author returned successfully");
            return new OkObjectResult(authorDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting author");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
