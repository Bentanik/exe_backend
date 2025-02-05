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

     public sealed class UserNotExistException : NotFoundException
    {
        public UserNotExistException()
                : base(AuthMessage.EmailNotExistException.GetMessage().Message,
                    AuthMessage.EmailNotExistException.GetMessage().Code)
        { }
    }

         public sealed class PasswordNotMatchException : BadRequestException
    {
        public PasswordNotMatchException()
                : base(AuthMessage.PasswordNotMatchException.GetMessage().Message,
                    AuthMessage.PasswordNotMatchException.GetMessage().Code)
        { }
    }
}
