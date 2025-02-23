using exe_backend.Contract.DTOs.SubscriptionDTOs;

namespace exe_backend.Contract.DTOs.UserDTOs;

public record UserDto(Guid? Id = null, string? IdentityId = null, string? Email = null, string? FullName = null, string? Avatar = null, SubscriptionDTO? Subscription = null, SubscriptionPackageDTO? SubscriptionPackage = null);