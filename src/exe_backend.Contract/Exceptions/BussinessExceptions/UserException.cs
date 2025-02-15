using exe_backend.Contract.Common.Messages;

namespace exe_backend.Contract.Exceptions.BussinessExceptions;

public static class UserException
{
    public sealed class SubscriptionActivedException : BadRequestException
    {
        public SubscriptionActivedException()
            : base(UserMessage.SubscriptionActivedException.GetMessage().Message,
            UserMessage.SubscriptionActivedException.GetMessage().Code)
        { }
    }

    public sealed class SubscriptionCancelFailException : BadRequestException
    {
        public SubscriptionCancelFailException()
            : base(UserMessage.SubscriptionCancelFailException.GetMessage().Message,
            UserMessage.SubscriptionCancelFailException.GetMessage().Code)
        { }
    }

    public sealed class SubscriptionNotRenewException : BadRequestException
    {
        public SubscriptionNotRenewException()
            : base(UserMessage.SubscriptionNotRenewException.GetMessage().Message,
            UserMessage.SubscriptionNotRenewException.GetMessage().Code)
        { }
    }
}