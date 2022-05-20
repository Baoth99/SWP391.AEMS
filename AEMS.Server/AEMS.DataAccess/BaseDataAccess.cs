using AEMS.Data.EF.UnitOfWork;
using AEMS.ORM.Dapper;

namespace AEMS.DataAccess
{
    public class BaseDataAccess
    {
        ///// <summary>
        ///// Gets the unit of work.
        ///// </summary>
        ///// <value>
        ///// The unit of work.
        ///// </value>
        protected IUnitOfWork UnitOfWork { get; private set; }

        ///// <summary>
        ///// Gets the dapper service.
        ///// </summary>
        ///// <value>
        ///// The dapper service.
        ///// </value>
        protected IDapperService DapperService { get; private set; }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="BaseDataAccess"/> class.
        ///// </summary>
        ///// <param name="unitOfWork">The unit of work.</param>
        ///// <param name="dapperService">The dapper service.</param>
        public BaseDataAccess(IUnitOfWork unitOfWork, IDapperService dapperService)
        {
            UnitOfWork = unitOfWork;
            DapperService = dapperService;
        }
    }
}
