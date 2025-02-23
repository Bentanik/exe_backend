namespace exe_backend.Contract.DTOs.CourseDTOs;

public record ChapterWithNotLectureDTO
(
    Guid? Id = null,
    string? Name = null,
    string? Description = null,
    ImageDTO? Thumbnail = null,
    int? QuantityChapters = null
);