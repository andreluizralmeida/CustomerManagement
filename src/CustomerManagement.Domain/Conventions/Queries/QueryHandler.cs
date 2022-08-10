namespace CustomerManagement.Domain.Conventions.Queries;

using System.Threading;
using System.Threading.Tasks;
using MediatR;

public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}

