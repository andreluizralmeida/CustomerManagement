namespace CustomerManagement.Domain.Conventions.Commands;

using Flunt.Notifications;
using MediatR;

public abstract class BaseCommand : Notifiable, IBaseRequest
{
}

public abstract class Command<TResponse> : BaseCommand, IRequest<TResponse>
{
}