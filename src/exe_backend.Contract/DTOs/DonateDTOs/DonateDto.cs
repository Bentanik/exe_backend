using exe_backend.Contract.DTOs.UserDTOs;

namespace exe_backend.Contract.DTOs.DonateDTOs;

public record DonateDto(
    Guid? Id = null,
    DateTime? CreatedDate = null,
    Guid? UserId = null,
    long? OrderId = null,
    int? Amount = null,
    UserDto? User = null
);
