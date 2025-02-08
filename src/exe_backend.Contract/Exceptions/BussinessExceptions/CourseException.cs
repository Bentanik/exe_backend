using exe_backend.Contract.Common.Messages;

namespace exe_backend.Contract.Exceptions.BussinessExceptions;

public static class CourseException
{
    public sealed class CourseNameDuplicateException : BadRequestException
    {
        public CourseNameDuplicateException()
            : base(CourseMessage.CourseNameDuplicateException.GetMessage().Message,
            CourseMessage.CourseNameDuplicateException.GetMessage().Code)
        { }
    }
}