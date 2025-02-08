namespace exe_backend.Contract.Common.Messages;

public enum CourseMessage
{
    [Message("Tên khóa học đã bị trùng", "course01")]
    CourseNameDuplicateException,
}