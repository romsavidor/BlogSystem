using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Dtos;

public class PostRequestDto
{
    public int AuthorId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [MinLength(100, ErrorMessage = "Content must be at least 100 characters")]
    public string Content { get; set; }
}
