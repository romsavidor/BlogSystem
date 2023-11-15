namespace BlogSystem.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public Author(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Posts = new List<Post>();
    }

    public List<Post> Posts { get; set; }
}


