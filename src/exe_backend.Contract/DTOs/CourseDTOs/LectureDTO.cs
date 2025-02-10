namespace exe_backend.Contract.DTOs.CourseDTOs;

public record LectureDTO
(
    Guid? Id = null,
    Guid? ChapterId = null,
    string? Name = null,
    string? Description = null,
    ImageDTO? Image = null,
    VideoDTO? Video = null
);