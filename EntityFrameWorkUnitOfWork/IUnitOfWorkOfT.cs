using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkUnitOfWork
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        /// <summary>

        /// Gets the db context.

        /// </summary>

        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>

        TContext DbContext { get; }
    }
}