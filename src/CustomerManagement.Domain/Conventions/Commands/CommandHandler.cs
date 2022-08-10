namespace CustomerManagement.Domain.Conventions.Commands;

using System.Threading;
using System.Threading.Tasks;
using MediatR;

public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}