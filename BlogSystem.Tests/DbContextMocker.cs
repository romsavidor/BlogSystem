using BlogSystem.Data;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BlogSystem.Tests;

public static class DbContextMocker
{
    public static Mock<BlogDbContext> GetBlogDbContextMock()
    {
        var posts = new Mock<DbSet<Post>>();
        var authors = new Mock<DbSet<Author>>();

        var contextMock = new Mock<BlogDbContext>();
        contextMock.Setup(c => c.Posts).Returns(posts.Object);
        contextMock.Setup(c => c.Authors).Returns(authors.Object);

        return contextMock;
    }
}
