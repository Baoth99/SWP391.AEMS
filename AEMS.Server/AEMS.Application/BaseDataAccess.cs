using AEMS.Data.EF.UnitOfWork;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using Microsoft.ApplicationInsights;

namespace AEMS.Application
{
    public class BaseDataAccess
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        protected IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the dapper service.
        /// </summary>
        /// <value>
        /// The dapper service.
        /// </value>
        protected IDapperService DapperService { get; private set; }

        /// <summary>
        /// Gets the authentication session.
        /// </summary>
        /// <value>
        /// The authentication session.
        /// </value>
        protected IAuthSession AuthSession { get; private set; }

        /// <summary>
        /// The telemetry client
        /// </summary>
        protected readonly TelemetryClient TelemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataAccess"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dapperService">The dapper service.</param>
        /// <param name="authSession">The authentication session.</param>
        public BaseDataAccess(IUnitOfWork unitOfWork, IDapperService dapperService, IAuthSession authSession, TelemetryClient telemetryClient)
        {
            UnitOfWork = unitOfWork;
            DapperService = dapperService;
            AuthSession = authSession;
            TelemetryClient = telemetryClient;
        }
    }
}
