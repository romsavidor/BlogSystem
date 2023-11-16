using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly BlogDbContext _context;

    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger, BlogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("/post")]
    public IActionResult CreatePost([FromBody] PostRequestDto postDto)
    {
        try
        {
            // TODO: Add validation (author ID, content/title length, etc)
            var newPost = new Post
            {
                AuthorId = postDto.AuthorId,
                Title = postDto.Title,
                Description = postDto.Description,
                Content = postDto.Content,
            };

            _context.Posts.Add(newPost);
            _context.SaveChanges();

            _logger.LogInformation("Post created successfully");
            return Ok($"Post with id {newPost.Id} created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating post");
            return StatusCode(500, "Internal Server Error");
        }

    }
}