namespace BlogSystem.Models;

public class Post
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title {  get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
 
    public Post(int authorId, string title, string description, string content)
    {
        Id = 0;
        AuthorId = authorId;
        Title = title;
        Description = description;
        Content = content;
    }

    public Author? Author { get; set; }
}
