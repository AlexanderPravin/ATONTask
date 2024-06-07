namespace Application.DTO;

public struct UpdateUserDTO
{
    public string Login { get; set; }
    
    public string Name { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime? BirthDay { get; set; }
}