namespace Infrastructure.Repositories;

/// <summary>
/// Represents a base repository for managing entities in the database.
/// Implements the IRepository interface.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public class BaseRepository<T>(Context context) : IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// The database context.
    /// </summary>
    protected readonly Context Context = context;

    /// <summary>
    /// Retrieves an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async Task<T?> GetById(Guid id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Retrieves an entity by a specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Retrieves a collection of entities by a specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A collection of entities.</returns>
    public async Task<ICollection<T>> GetCollectionBy(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    public async Task<ICollection<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task Add(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Updates an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }
}