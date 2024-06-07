namespace Infrastructure;

/// <summary>
/// Represents a unit of work that groups together changes to the database context.
/// </summary>
public class UnitOfWork(Context context, IRepository<User> userRepository)
{
    /// <summary>
    /// Gets the UserRepository that this unit of work operates on.
    /// </summary>
    public IRepository<User> UserRepository { get; } = userRepository;

    /// <summary>
    /// The database context.
    /// </summary>
    private readonly Context _context = context;

    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}