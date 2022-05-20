using AEMS.Utilities;
using MediatR;

namespace AEMS.Application
{
    public class BaseService 
    {
        /// <summary>
        /// The mediator
        /// </summary>
        protected IMediator Mediator { get; private set; }

        /// <summary>
        /// Gets the user authentication session.
        /// </summary>
        /// <value>
        /// The user authentication session.
        /// </value>
        protected IAuthSession UserAuthSession { get; private set; }

        public BaseService(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
