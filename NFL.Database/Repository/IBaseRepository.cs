using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NFL.Database
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Begin a non-async database transaction
        /// </summary>
        /// <param name="memberName">(Optional) Method/Property calling this function</param>
        /// <returns>Transaction</returns>
        TransactionScope BeginTransaction([CallerMemberName] string memberName = "");

        /// <summary>
        /// Begin an async database transaction
        /// </summary>
        /// <param name="memberName">(Optional) Method/Property calling this function</param>
        /// <returns>Transaction</returns>
        TransactionScope BeginAsyncTransaction([CallerMemberName] string memberName = "");
    }
}
