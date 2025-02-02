using MediatR;

namespace exe_backend.Contract.Abstractions.Message;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
