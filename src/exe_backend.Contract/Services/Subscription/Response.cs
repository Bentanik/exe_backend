using exe_backend.Contract.DTOs.SubscriptionDTOs;

namespace exe_backend.Contract.Services.Subscription;


public static class Response
{
    public record SubscriptionPackageResponse
        (SubscriptionPackageDTO SubscriptionPackage);

    public record SubscriptionPackagesResponse
     (PagedResult<SubscriptionPackageDTO> SubscriptionPackages);
}
