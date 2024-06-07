namespace Application.DTO;

public class CreateUserDTO
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDay { get; set; }

    public bool IsAdmin { get; set; }
}