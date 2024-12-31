using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Agora.Common.EFCore;

/// <summary>
/// Represents a unit of work in the context of Entity Framework, managing the lifecycle of a DbContext transaction.
/// </summary>
public class UnitOfWork : IDisposable
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the UnitOfWork class with the specified DbContext.
    /// </summary>
    /// <param name="context">The DbContext to be used in this transaction. It is responsible for committing changes to the database.</param>
    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Commits all changes made in the context to the database asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Begins a transaction on the underlying database.
    /// </summary>
    /// <returns></returns>
    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    private bool _disposed = false;

    /// <summary>
    /// Disposes the DbContext if it hasn't been disposed already.
    /// </summary>
    /// <param name="disposing">Indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    /// <summary>
    /// Disposes the DbContext, freeing up resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
