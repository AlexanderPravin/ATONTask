namespace Domain.Entities;

/// <summary>
/// Represents a User entity in the system.
/// Inherits from the BaseEntity class.
/// </summary>
public sealed class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the login of the user.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the gender of the user.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Gets or sets the birthday of the user.
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is an admin.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user who created this user.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the date and time when the user was last modified.
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// Gets or sets the user who last modified this user.
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was revoked.
    /// </summary>
    public DateTime? RevokedOn { get; set; }

    /// <summary>
    /// Gets or sets the user who revoked this user.
    /// </summary>
    public string? RevokedBy { get; set; }
}