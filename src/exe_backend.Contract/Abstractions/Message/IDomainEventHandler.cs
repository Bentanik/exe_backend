using MediatR;

namespace exe_backend.Contract.Abstractions.Message;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
