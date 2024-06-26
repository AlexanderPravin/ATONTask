﻿namespace Application.DTO;

public class UpdateUserDTO
{
    public string Login { get; set; }
    
    public string Name { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime? BirthDay { get; set; }
}