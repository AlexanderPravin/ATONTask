namespace Application.DTO;

public class ResponseUserDTO
{
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public Gender Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    public bool IsAdmin { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime? ModifiedOn { get; set; }
    
    public string? ModifiedBy { get; set; }
    
    public DateTime? RevokedOn { get; set; }

    public string? RevokedBy { get; set; }
}