namespace exe_backend.Contract.Common.Messages;

public enum CourseMessage
{
    [Message("Tên khóa học đã bị trùng", "course01")]
    CourseNameDuplicateException,

    [Message("Đã lưu Thumbnail thành công", "course02")]
    SaveThumbnailCourseSuccessfully
}