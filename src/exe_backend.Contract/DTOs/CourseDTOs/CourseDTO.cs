using exe_backend.Contract.DTOs.CategoryDTOs;
using exe_backend.Contract.DTOs.LevelDTOs;

namespace exe_backend.Contract.DTOs.CourseDTOs;

public record CourseDTO
(
    Guid? Id = null,
    string? Name = null,
    string? Description = null,
    ImageDTO? Thumbnail = null,
    int? QuantityChapters = null,
    CategoryDTO? Category = null,
    LevelDTO? Level = null,
    ChapterDTO[]? Chapters = null
);