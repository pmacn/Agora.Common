using Agora.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Agora.Common.EFCore;

/// <summary>
/// Base class for repositories that interact with a database context for a specific entity type.
/// </summary>
/// <typeparam name="T">The type of the aggregate root entity that this repository handles.</typeparam>
public abstract class Repository<T> where T : AggregateRoot
{
    protected readonly DbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the Repository class with the provided database context.
    /// </summary>
    /// <param name="dbContext">The database context used for data access.</param>
    protected Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    public async Task<T?> FindAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
}
