using BlogSystem.Dtos;
using BlogSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost("/author")]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorRequestDto authorDto)
    {
        return await _authorService.CreateAuthorAsync(authorDto);
    }

    [HttpGet("/author/{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        return await _authorService.GetAuthorAsync(id);
    }
}
