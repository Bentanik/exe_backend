namespace exe_backend.Contract.Common.Messages;

public enum CourseMessage
{
    [Message("Tên khóa học đã bị trùng", "course01")]
    CourseNameDuplicateException,

    [Message("Lưu thông tin khóa học thành công, đang lưu ảnh", "course02")]
    SaveCourseSuccessfully,

    [Message("Đã lưu Thumbnail thành công", "course03")]
    SaveThumbnailCourseSuccessfully,

    [Message("Khóa học không tìm thấy", "course04")]
    CourseNotFoundException,

    [Message("Đã lưu chương học thành công", "chapter01")]
    CreateChapterSuccessfully,

    [Message("Chương học không tìm thấy", "chapter02")]
    ChapterNotFoundException,

    [Message("Lưu thông tin chương học thành công, đang lưu ảnh và video", "lecture01")]
    SaveLectureSuccessfully,

    [Message("Đã lưu ảnh và video của bài học thành công", "lecture02")]
    SaveImageAndVideoLectureSuccessfully
}