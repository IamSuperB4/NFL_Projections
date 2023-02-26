using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database
{
    public interface INflTransaction : IDisposable
    {
        Task CommitAsync();

        Task RollbackAsync();
    }

    class NflTransaction : INflTransaction, IDisposable
    {
        private IDbContextTransaction? transaction = null;

        public NflTransaction(NflContext context)
        {
            // initiate transaction
            transaction = context.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits a transaction to the database
        /// </summary>
        /// <exception cref="Exception">No active transaction found</exception>
        async public Task CommitAsync()
        {
            if (transaction == null)
            {
                throw new Exception("No active transaction found");
            }

            await transaction.CommitAsync();
            transaction = null;
        }

        /// <summary>
        /// Rollback a transaction
        /// </summary>
        async public Task RollbackAsync()
        {
            try
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                    transaction = null;
                }
            }
            catch
            {
                // ignore the error
            }
        }

        /// <summary>
        /// Dispose of and rollback a transaction
        /// </summary>
        public void Dispose()
        {
            RollbackAsync().Wait();
        }

        /// <summary>
        /// Dispose of and rollback a transaction
        /// </summary>
        ~NflTransaction()
        {
            RollbackAsync().Wait();
        }
    }
}
