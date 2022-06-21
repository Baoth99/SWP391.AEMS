using MediatR;

namespace AEMS.Application
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
