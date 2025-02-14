namespace exe_backend.Contract.DTOs.LevelDTOs;
public record LevelDTO
(
    Guid? Id = null,
    string? Name = null,
    int? QuantityCourses = null
);