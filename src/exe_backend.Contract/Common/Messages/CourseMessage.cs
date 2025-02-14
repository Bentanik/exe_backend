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

    [Message("Đã lấy thông tin khóa học thành công", "course05")]
    GetCourseSuccessfully,

    [Message("Đã lưu chương học thành công", "chapter01")]
    CreateChapterSuccessfully,

    [Message("Chương học không tìm thấy", "chapter02")]
    ChapterNotFoundException,

    [Message("Đã lấy chương học thành công", "chapter03")]
    GetChapterSuccessfully,

    [Message("Lưu thông tin chương học thành công, đang lưu ảnh và video", "lecture01")]
    SaveLectureSuccessfully,

    [Message("Đã lưu ảnh và video của bài học thành công", "lecture02")]
    SaveImageAndVideoLectureSuccessfully,

    [Message("Đã lấy bài học thành công", "lecture03")]
    GetLectureSuccessfully

}