using BlogSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Services;

public interface IPostService
{
    Task<IActionResult> CreatePostAsync(PostRequestDto postDto);
    Task<IActionResult> GetPostAsync(int id, bool includeAuthor = false);
}
