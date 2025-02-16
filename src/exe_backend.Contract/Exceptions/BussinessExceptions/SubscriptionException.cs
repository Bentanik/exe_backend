using exe_backend.Contract.Common.Messages;

namespace exe_backend.Contract.Exceptions.BussinessExceptions;

public static class SubscriptionException
{
    public sealed class SubscriptionNotFoundException : NotFoundException
    {
        public SubscriptionNotFoundException()
            : base(SubscriptionMessage.GetSubscriptionNotFoundException.GetMessage().Message,
            SubscriptionMessage.GetSubscriptionNotFoundException.GetMessage().Code)
        { }
    }
}