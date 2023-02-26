using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Serilog;

namespace NFL.Database
{
    /// <summary>
    /// Type of Upset action (Update, Insert, None)
    /// </summary>
    enum UpsertAction
    {
        None,
        Insert,
        Update
    }

    /// <summary>
    /// Base repository in charge of creating DbContexts and database Transactions
    /// </summary>
    /// <typeparam name="T">Type of Repository</typeparam>
    abstract class BaseRepository<T> : IBaseRepository, IDisposable where T : BaseDbContext
    {
        protected readonly ILogger _logger;
        private readonly T _context;

        protected BaseRepository(T context, ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Creates a DbContext
        /// </summary>
        /// <param name="memberName">(Optional) Method/Property calling this function</param>
        /// <returns>DbContext</returns>
        protected T CreateDbContext([CallerMemberName] string memberName = "")
        {
            return _context;
        }

        /// <summary>
        /// Begin a non-async database transaction
        /// </summary>
        /// <param name="memberName">(Optional) Method/Property calling this function</param>
        /// <returns>Transaction</returns>
        public TransactionScope BeginTransaction([CallerMemberName] string memberName = "")
        {
            _logger.Information($"BeginTransaction: called by {memberName}");
            return new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    });
        }

        /// <summary>
        /// Begin an async database transaction
        /// </summary>
        /// <param name="memberName">(Optional) Method/Property calling this function</param>
        /// <returns>Transaction</returns>
        public TransactionScope BeginAsyncTransaction([CallerMemberName] string memberName = "")
        {
            _logger.Information($"BeginTransactionAsync: called by {memberName}");
            return new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled);
        }

        /// <summary>
        /// Dispose of Repository
        /// </summary>
        public void Dispose()
        {
        }
    }
}
