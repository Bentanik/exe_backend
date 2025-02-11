namespace exe_backend.Contract.DTOs.CourseDTOs;

public record ChapterDTO
(
    Guid? Id = null,
    Guid? CourseId = null,
    string? Name = null,
    string? Description = null,
    int? QuantityLectures = null,
    double? TotalDurationLectures = null
);