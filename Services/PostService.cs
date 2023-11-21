using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Services;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    private readonly ILogger<PostService> _logger;

    public PostService(ILogger<PostService> logger, BlogDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> CreatePostAsync(PostRequestDto postDto)
    {
        try
        {
            // Validate AuthorId
            var existingAuthor = await _context.Authors.FindAsync(postDto.AuthorId);
            if (existingAuthor == null)
            {
                _logger.LogError("Author with the specified Id does not exist");
                return new BadRequestObjectResult("Author with the specified Id does not exist");
            }

            var newPost = new Post(postDto.AuthorId, postDto.Title, postDto.Description, postDto.Content);

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Post created successfully");
            return new OkObjectResult($"Post with id {newPost.Id} created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating post");
            return new StatusCodeResult(500);
        }
    }

    public async Task<IActionResult> GetPostAsync(int id, bool includeAuthor = false)
    {
        try
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                _logger.LogError("Post not found");
                return new NotFoundResult();
            }

            var postDto = new PostResponseDto(post.Id, post.Title, post.Description, post.Content);

            if (includeAuthor)
            {
                _context.Entry(post).Reference(p => p.Author).Load();

                if (post.Author != null)
                {
                    postDto.Author = new AuthorResponseDto(post.Author.Id, post.Author.Name, post.Author.Surname);
                }
                else
                {
                    _logger.LogError("Author not found for the requested post");
                    return new NotFoundResult();
                }
            }

            _logger.LogInformation("Post returned successfully");
            return new OkObjectResult(postDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting post");
            return new StatusCodeResult(500);
        }
    }
}
