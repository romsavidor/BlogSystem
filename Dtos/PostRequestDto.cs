namespace BlogSystem.Dtos;

public class PostRequestDto
{
    // TODO: add validation rules?
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}
