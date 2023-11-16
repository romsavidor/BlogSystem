namespace BlogSystem.Dtos;

public class PostResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public AuthorResponseDto? Author { get; set; }
}
