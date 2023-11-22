using BlogSystem.Data;
using BlogSystem.Dtos;
using BlogSystem.Services;
using BlogSystem.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogSystem.Tests.Services;

public class AuthorServiceTests
{
    [Fact]
    public async Task CreateAuthorAsync_ValidData_ReturnsOkResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthorServiceTestDatabase1")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var loggerMock = new Mock<ILogger<AuthorService>>();
            var authorService = new AuthorService(context, loggerMock.Object);

            var authorDto = new AuthorRequestDto
            {
                Name = "TestAuthor",
                Surname = "TestSurname"
            };

            // Act
            var result = await authorService.CreateAuthorAsync(authorDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }

    [Fact]
    public async Task GetAuthorAsync_ExistingAuthor_ReturnsOkResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthorServiceTestDatabase2")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            // Seed data
            var testSeedData = new TestSeedData(context);
            await testSeedData.SeedTestDataAsync(true, false);

            var loggerMock = new Mock<ILogger<AuthorService>>();
            var authorService = new AuthorService(context, loggerMock.Object);

            // Act
            var result = await authorService.GetAuthorAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var authorDto = Assert.IsType<AuthorResponseDto>(okResult.Value);

            Assert.Equal(1, authorDto.Id);
            Assert.Equal(testSeedData.SeededAuthor.Name, authorDto.Name);
            Assert.Equal(testSeedData.SeededAuthor.Surname, authorDto.Surname);
        }
    }

    [Fact]
    public async Task GetAuthorAsync_NonExistingAuthor_ReturnsNotFoundResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthorServiceTestDatabase3")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var loggerMock = new Mock<ILogger<AuthorService>>();
            var authorService = new AuthorService(context, loggerMock.Object);

            // Act
            var result = await authorService.GetAuthorAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task CreateAuthorAsync_ExceptionThrown_ReturnsInternalServerError()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthorServiceTestDatabase4")
            .Options;

        using (var context = new BlogDbContext(options))
        {
            var loggerMock = new Mock<ILogger<AuthorService>>();
            var authorService = new AuthorService(context, loggerMock.Object);

            var invalidAuthorDto = new AuthorRequestDto();

            // Act
            var result = await authorService.CreateAuthorAsync(invalidAuthorDto);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, (result as StatusCodeResult)?.StatusCode);
        }
    }    
}
