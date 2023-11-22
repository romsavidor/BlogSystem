using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Services;
using BlogSystem.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogSystem.Tests.Services;

public class PostServiceTests
{
    [Fact]
    public async Task CreatePostAsync_ValidData_ReturnsOkResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "FirstTestDatabase")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var testSeedData = new TestSeedData(context);
            await testSeedData.SeedTestDataAsync();

            var loggerMock = new Mock<ILogger<PostService>>();
            var postService = new PostService(loggerMock.Object, context);

            var postDto = new PostRequestDto
            {
                AuthorId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Content = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m"
            };

            // Act
            var result = await postService.CreatePostAsync(postDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }

    [Fact]
    public async Task CreatePost_InvalidAuthor_ReturnsBadRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "SecondTestDatabase")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var loggerMock = new Mock<ILogger<PostService>>();
            var postService = new PostService(loggerMock.Object, context);

            // Act
            var result = await postService.CreatePostAsync(new PostRequestDto
            {
                AuthorId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Content = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m"
            });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Author with the specified Id does not exist", (result as BadRequestObjectResult)?.Value);
        }
    }

    [Fact]
    public async Task GetPost_ExistingPost_ReturnsOk()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "ThirdTestDatabase")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            // Seed data
            var testSeedData = new TestSeedData(context);
            await testSeedData.SeedTestDataAsync(true, true);

            var loggerMock = new Mock<ILogger<PostService>>();
            var postService = new PostService(loggerMock.Object, context);

            // Act
            var result = await postService.GetPostAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }

    [Fact]
    public async Task GetPost_NonExistingPost_ReturnsNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "FourthTestDatabase")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var loggerMock = new Mock<ILogger<PostService>>();
            var postService = new PostService(loggerMock.Object, context);

            // Act
            var result = await postService.GetPostAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task GetPost_ExistingPost_IncludeAuthor_ReturnsOk()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "FifthTestDatabase")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            // Seed data
            var testSeedData = new TestSeedData(context);
            await testSeedData.SeedTestDataAsync(true, true);

            var loggerMock = new Mock<ILogger<PostService>>();
            var postService = new PostService(loggerMock.Object, context);

            // Act
            var result = await postService.GetPostAsync(1, true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var postDto = Assert.IsType<PostResponseDto>(okResult.Value);

            Assert.NotNull(postDto.Author);
            Assert.Equal(1, postDto.Author.Id);
            Assert.Equal(testSeedData.SeededAuthor.Name, postDto.Author.Name);
            Assert.Equal(testSeedData.SeededAuthor.Surname, postDto.Author.Surname);
        }
    }
}
