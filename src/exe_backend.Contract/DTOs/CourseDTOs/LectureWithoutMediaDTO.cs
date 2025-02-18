namespace exe_backend.Contract.DTOs.CourseDTOs;

public record LectureWithoutMediaDTO
(
    Guid? Id = null,
    Guid? ChapterId = null,
    string? Name = null,
    string? Description = null
);