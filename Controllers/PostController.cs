using BlogSystem.Dtos;
using BlogSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost("/post")]
    public async Task<IActionResult> CreatePost([FromBody] PostRequestDto postDto)
    {
        return await _postService.CreatePostAsync(postDto);
    }

    [HttpGet("/post/{id}")]
    public async Task<IActionResult> GetPost(int id, [FromQuery] bool includeAuthor = false)
    {
        return await _postService.GetPostAsync(id, includeAuthor);
    }
}