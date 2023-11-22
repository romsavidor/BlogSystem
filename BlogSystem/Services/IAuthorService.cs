using BlogSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Services;

public interface IAuthorService
{
    Task<IActionResult> CreateAuthorAsync(AuthorRequestDto authorDto);
    Task<IActionResult> GetAuthorAsync(int id);
}
