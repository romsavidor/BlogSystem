using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    // TODO: Move calls logic to handlers?
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
            // TODO: restrict only using existing author ids?
            var newPost = new Post(postDto.AuthorId, postDto.Title, postDto.Description, postDto.Content);

            // TODO: should Author be assigned here?
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

    [HttpGet("/post/{id}")]
    public IActionResult GetPost(int id, [FromQuery] bool includeAuthor = false)
    {
        try
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                _logger.LogError("Post not found");
                return NotFound();
            }
                

            var postDto = new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content
            };

            if (includeAuthor)
            {
                _context.Entry(post).Reference(p => p.Author).Load();

                if (post.Author != null)
                {
                    postDto.Author = new AuthorResponseDto
                    {
                        Id = post.Author.Id,
                        Name = post.Author.Name,
                        Surname = post.Author.Surname
                    };
                }
                else
                {
                    _logger.LogError("Author not found for the requested post");
                    return NotFound();
                }
                
            }

            _logger.LogInformation("Post returned successfully");
            return Ok(postDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting post");
            return StatusCode(500, "Internal Server Error");
        }
    }
}