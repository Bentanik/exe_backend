namespace exe_backend.Contract.DTOs.SubscriptionDTOs;

public record SubscriptionDTO
(
    Guid? Id = null,
    Guid? SubscriptionPackageId = null,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    Guid? UserId = null
);