namespace exe_backend.Contract.DTOs.CategoryDTOs;

public record CategoryDTO
(
    Guid? Id = null,
    string? Name = null,
    int? QuantityCourses = null
);