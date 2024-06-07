namespace Domain.Common;

// Abstract base class for all entities
public abstract class BaseEntity
{
    // Unique identifier for each entity
    public Guid Id { get; set; }
}