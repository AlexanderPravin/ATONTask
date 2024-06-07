namespace Domain.Interfaces;

/// <summary>
/// IRepository is a generic interface that provides basic CRUD operations for any entity type.
/// </summary>
/// <typeparam name="T">The type of the entity. This type parameter is constrained to BaseEntity.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Asynchronously gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetById(Guid id);

    /// <summary>
    /// Asynchronously gets an entity by a specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetBy(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Asynchronously gets a collection of entities by a specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entities.</param>
    /// <returns>A collection of entities that satisfy the predicate.</returns>
    Task<ICollection<T>> GetCollectionBy(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Asynchronously gets all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<ICollection<T>> GetAll();

    /// <summary>
    /// Asynchronously adds an entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task Add(T entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);
}