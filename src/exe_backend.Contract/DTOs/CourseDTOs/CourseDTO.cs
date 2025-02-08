namespace exe_backend.Contract.DTOs.CourseDTOs;

public record CourseDTO
(
    Guid? Id = null,
    string? Name = null,
    string? Description = null,
    ImageDTO? Thumbnail = null,
    int? QuantityChapters = null
);