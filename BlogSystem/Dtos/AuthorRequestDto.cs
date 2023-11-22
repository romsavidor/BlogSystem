using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Dtos;

public class AuthorRequestDto
{
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; }

    [MinLength(2, ErrorMessage = "Surname must be at least 2 characters")]
    public string Surname { get; set; }
}
