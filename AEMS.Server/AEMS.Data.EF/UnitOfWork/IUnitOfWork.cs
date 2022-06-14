using AEMS.Data.EF.Repositories;
using AEMS.Data.Entities;
using System.Threading.Tasks;
using System.Transactions;

namespace AEMS.Data.EF.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories

        IRepository<Device> DeviceRepository { get; }

        IRepository<DeviceCategory> DeviceCategoryRepository { get; }

        IRepository<Area> AreaRepository { get; }

        IRepository<Photo> PhotoRepository { get; }

        #endregion

        #region Transaction Method

        /// <summary>
        /// Sets the isolation level.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void SetIsolationLevel(IsolationLevel isolationLevel);

        /// <summary>
        /// Sets the transaction scope option.
        /// </summary>
        /// <param name="transactionScopeOption">The transaction scope option.</param>
        void SetTransactionScopeOption(TransactionScopeOption transactionScopeOption);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        void RollbackTransaction();

        #endregion

        #region Commit Async to Database

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        #endregion
    }
}
