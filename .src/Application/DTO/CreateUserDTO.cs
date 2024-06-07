namespace Application.DTO;

public struct CreateUserDTO
{
    public string Login;

    public string Password;

    public string Name;

    public Gender Gender;

    public DateTime? BirthDay;

    public bool IsAdmin;
}