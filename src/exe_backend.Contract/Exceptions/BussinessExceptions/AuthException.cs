using exe_backend.Contract.Common.Messages;

namespace exe_backend.Contract.Exceptions.BussinessExceptions;

public static class AuthException
{
    public sealed class UserExistException : BadRequestException
    {
        public UserExistException()
                : base(AuthMessage.EmailExistException.GetMessage().Message,
                    AuthMessage.EmailExistException.GetMessage().Code)
        { }
    }
}
