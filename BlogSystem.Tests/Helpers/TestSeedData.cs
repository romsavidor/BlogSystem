using BlogSystem.Data;
using BlogSystem.Models;

namespace BlogSystem.Tests.Helpers;

public class TestSeedData : IDisposable
{
    public Author SeededAuthor { get; private set; }
    public Post SeededPost { get; private set; }

    private readonly BlogDbContext _context;

    public TestSeedData(BlogDbContext context)
    {
        SeededAuthor = new Author("TestAuthor", "TestSurname");
        SeededPost = new Post(1, "Test Title", "Test Description",
                "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m");
        _context = context;
    }

    public async Task SeedTestDataAsync(bool includeAuthor = true, bool includePost = true)
    {
        if (includeAuthor)
        {
            _context.Authors.Add(SeededAuthor);
        }

        if (includePost)
        {
            _context.Posts.Add(SeededPost);
        }

        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
