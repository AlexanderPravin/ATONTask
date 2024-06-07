namespace Application.DTO;

public class SelfUserDTO
{
    public string Name { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public bool IsRevoked { get; set; }
}