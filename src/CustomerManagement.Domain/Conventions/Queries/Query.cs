namespace CustomerManagement.Domain.Conventions.Queries;

using Flunt.Notifications;
using MediatR;

public abstract class Query<TResponse> : Notifiable, IRequest<TResponse>
{
}
