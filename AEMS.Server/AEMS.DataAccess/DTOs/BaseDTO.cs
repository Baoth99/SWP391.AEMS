using MediatR;

namespace AEMS.DataAccess.DTOs
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
