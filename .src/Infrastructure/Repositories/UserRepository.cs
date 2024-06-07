namespace Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing User entities in the database.
/// Inherits from the BaseRepository class.
/// </summary>
public class UserRepository(Context context) : BaseRepository<User>(context);