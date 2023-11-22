namespace BlogSystem.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public Author(string name, string surname)
    {
        Id = 0;
        Name = name;
        Surname = surname;
        Posts = new List<Post>();
    }

    public List<Post> Posts { get; set; }
}


