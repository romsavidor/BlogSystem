using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the one-to-many relationship between Author and Post
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Posts)
            .WithOne(post => post.Author)
            .HasForeignKey(post => post.AuthorId);
    }
}
