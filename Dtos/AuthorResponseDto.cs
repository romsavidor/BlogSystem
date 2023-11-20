namespace BlogSystem.Dtos;

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public AuthorResponseDto(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }
}
