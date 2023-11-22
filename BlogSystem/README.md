# BlogSystem

## Overview

This solution, represented by the `BlogSystem.sln` file, is a simple blogging system implemented in C# using ASP.NET Core. The project structure is designed to showcase key components of a web application, including controllers, data models, services, and testing.

### Project Structure

- **BlogSystem (Project)**
  - **Controllers**
    - **AuthorController:** Handles author-related operations.
    - **PostController:** Handles post-related operations.
  - **Data**
    - **BlogDbContext:** Represents the database context for the application.
  - **Dtos**
    - **AuthorRequestDto:** Data transfer object for author creation requests.
    - **AuthorResponseDto:** Data transfer object for author responses.
    - **PostRequestDto:** Data transfer object for post creation requests.
    - **PostResponseDto:** Data transfer object for post responses.
  - **Migrations**
    - Database migration files for Entity Framework Core.
  - **Models**
    - **Author:** Represents an author in the blogging system.
    - **Post:** Represents a post in the blogging system.
  - **Services**
    - **AuthorService:** Implements author-related business logic.
    - **IAuthorService:** Interface for author-related services.
    - **IPostService:** Interface for post-related services.
    - **PostService:** Implements post-related business logic.
  - **Program.cs:** Entry point for the application.

- **BlogSystem.Tests (Project)**
  - **Helpers**
    - **TestSeedData:** A utility class for seeding test data in the database.
  - **Services**
    - **AuthorServiceTests:** Unit tests for the AuthorService.
    - **PostServiceTests:** Unit tests for the PostService.
  - **DbContextMocker:** A helper class for mocking the database context in tests.

## Design Choices and Considerations

### In-Memory Database for Tests

In the testing project, an in-memory database is used (`UseInMemoryDatabase`) for efficient and isolated unit testing. This ensures that tests run quickly and without affecting the actual database.

### Lack of End-to-End (E2E) Tests

E2E tests with an actual database are not included in this project due to its simplicity. In a larger project, E2E tests could be added to validate the entire system's functionality with a real database.

### Entity Framework (EF)

EF Core is chosen as the Object-Relational Mapping (ORM) tool for database interactions. It simplifies database operations, promotes code-first development, and allows for easy migrations.

### DTO Validations

Validation attributes are applied to request DTOs (`AuthorRequestDto` and `PostRequestDto`). These validations ensure that the incoming data adheres to specified rules.

### Preventing Addition of Posts without an Author

A design choice is made to prevent the addition of posts without a specified author. This ensures data integrity and clarity in the relationship between authors and posts.

### Controller-Service Separation

The application follows the Controller-Service pattern. Controller logic is kept minimal, with the majority of business logic residing in service classes (`AuthorService` and `PostService`). This promotes separation of concerns, making the codebase more modular and testable.

### Simplicity in Duplicate Prevention

For simplicity, duplicate prevention for authors and posts is not implemented. In a more complex system, duplicate checks could be added based on specific requirements.

## Running the Application

To run the application, open the solution file (`BlogSystem.sln`) in Visual Studio or your preferred IDE. Set the appropriate startup project (e.g., `BlogSystem`) and start the application.

## Running Tests

Tests can be executed by running the test project (`BlogSystem.Tests`). Ensure that the correct test project is set as the startup project before running the tests.
