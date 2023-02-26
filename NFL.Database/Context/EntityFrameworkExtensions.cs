using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NFL.Database
{
    static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Creates a transaction (async)
        /// </summary>
        /// <returns>Transaction</returns>
        private static TransactionScope CreateAsyncTransaction()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled);
        }

        /// <summary>
        /// Creates a transaction (non-async)
        /// </summary>
        /// <returns>Transaction</returns>
        private static TransactionScope CreateTransaction()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    });
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (non-async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static List<T> ToListWithNoLock<T>(this IQueryable<T> query)
        {
            using (var scope = CreateTransaction())
            {
                List<T> result = query.ToList();
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query)
        {
            using (var scope = CreateAsyncTransaction())
            {
                List<T> result = await query.ToListAsync();
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock and with cancellation token (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            using (var scope = CreateAsyncTransaction())
            {
                List<T> result = await query.ToListAsync(cancellationToken);
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (non-async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static T FirstOrDefaultWithNoLock<T>(this IQueryable<T> query)
        {
            using (var scope = CreateTransaction())
            {
                T result = query.FirstOrDefault();
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<T> FirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query)
        {
            using (var scope = CreateAsyncTransaction())
            {
                T result = await query.FirstOrDefaultAsync();
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock and with cancellation token (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<T> FirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            using (var scope = CreateAsyncTransaction())
            {
                T result = await query.FirstOrDefaultAsync(cancellationToken);
                scope.Complete();

                return result;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (non-async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static int CountWithNoLock<T>(this IQueryable<T> query)
        {
            using (var scope = CreateTransaction())
            {
                int toReturn = query.Count();
                scope.Complete();

                return toReturn;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<int> CountWithNoLockAsync<T>(this IQueryable<T> query)
        {
            using (var scope = CreateAsyncTransaction())
            {
                int toReturn = await query.CountAsync();
                scope.Complete();

                return toReturn;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock and with cancellation token (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <returns>List of T model from database</returns>
        public static async Task<int> CountWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            using (var scope = CreateAsyncTransaction())
            {
                int toReturn = await query.CountAsync(cancellationToken);
                scope.Complete();

                return toReturn;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (non-async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <param name="expression">Expression for Max() Linq function, such as the field to find the max of</param>
        /// <returns>List of T model from database</returns>
        public static TResult MaxWithNoLock<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> expression)
        {
            using (var scope = CreateTransaction())
            {
                TResult toReturn = query.Max(expression);
                scope.Complete();

                return toReturn;
            }
        }

        /// <summary>
        /// ToList() IQueryable Linq function with no lock (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <param name="expression">Expression for Max() Linq function, such as the field to find the max of</param>
        /// <returns>List of T model from database</returns>
        public static async Task<TResult> MaxWithNoLockAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> expression)
        {
            using (var scope = CreateAsyncTransaction())
            {
                TResult toReturn = await query.MaxAsync(expression);
                scope.Complete();

                return toReturn;
            }
        }
        /// <summary>
        /// ToList() IQueryable Linq function with no lock and with cancellation token (async)
        /// </summary>
        /// <typeparam name="T">Database model</typeparam>
        /// <param name="query">Entity framework query</param>
        /// <param name="expression">Expression for Max() Linq function, such as the field to find the max of</param>
        /// <returns>List of T model from database</returns>

        public static async Task<TResult> MaxWithNoLockAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> expression, CancellationToken cancellationToken = default)
        {
            using (var scope = CreateAsyncTransaction())
            {
                TResult toReturn = await query.MaxAsync(expression, cancellationToken);
                scope.Complete();

                return toReturn;
            }
        }
    }
}
