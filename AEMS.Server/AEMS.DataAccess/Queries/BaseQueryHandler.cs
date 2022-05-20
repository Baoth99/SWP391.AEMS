using AEMS.DataAccess.DTOs;
using MediatR;

namespace AEMS.DataAccess.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
