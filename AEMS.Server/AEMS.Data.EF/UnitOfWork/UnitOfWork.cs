using AEMS.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AEMS.Data.EF.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region AppDbContext

        /// <summary>
        /// The application database context
        /// </summary>
        private readonly AppDbContext AppDbContext;

        #endregion

        #region Fields

        /// <summary>
        /// The isolation level
        /// </summary>
        private IsolationLevel? _isolationLevel;

        /// <summary>
        /// The transaction
        /// </summary>
        private IDbContextTransaction Transaction;

        /// <summary>
        /// The transaction scope option
        /// </summary>
        private TransactionScopeOption? _transactionScopeOption;

        #endregion

        #region Disposed

        /// <summary>
        /// The disposed
        /// </summary>
        private bool Disposed = false;

        #endregion

        #region Private variable Repositories




        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="appDbContext">The application database context.</param>
        public UnitOfWork(AppDbContext appDbContext)
        {
            //Initialize AppDbContext
            AppDbContext = appDbContext;
            //Set Command Timeout
            AppDbContext.Database.SetCommandTimeout(AppSettingValues.CommandTimeout);
        }

        #endregion

        #region Publish Access Repositories




        #endregion

        #region Transaction Methods

        /// <summary>
        /// Sets the isolation level for new transactions.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        /// <summary>
        /// Sets the transaction scope option.
        /// </summary>
        /// <param name="transactionScopeOption">The transaction scope option.</param>
        public void SetTransactionScopeOption(TransactionScopeOption transactionScopeOption)
        {
            _transactionScopeOption = transactionScopeOption;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                Transaction = AppDbContext.Database.BeginTransaction();

            }
            AppDbContext.Database.OpenConnection();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }

            AppDbContext.Database.CloseConnection();
        }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (Transaction == null)
            {
                return;
            }

            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
            _isolationLevel = null;
            AppDbContext.Database.CloseConnection();
        }

        #endregion

        #region Commit Async

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        public async Task CommitAsync()
        {
            var executionStrategy = AppDbContext.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                try
                {
                    BeginTransaction();
                    if (_isolationLevel.HasValue && _transactionScopeOption.HasValue)
                    {
                        using (var transactionScope = new TransactionScope(_transactionScopeOption.Value, new TransactionOptions() { IsolationLevel = _isolationLevel.Value }))
                        {
                            await AppDbContext.SaveChangesAsync();
                            transactionScope.Complete();
                        }
                    }
                    else
                    {
                        await AppDbContext.SaveChangesAsync();
                    }
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    throw ex;
                }
            });
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                }
            }
            this.Disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
