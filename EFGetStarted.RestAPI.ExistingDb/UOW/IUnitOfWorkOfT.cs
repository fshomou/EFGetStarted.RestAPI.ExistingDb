using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.UOW
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
