namespace exe_backend.Contract.DTOs.SubscriptionDTOs;

public record SubscriptionPackageDTO
(
    Guid? Id = null,
    string? Name = null,
    int? Price = null,
    DateTime? ExpiredDate = null
);