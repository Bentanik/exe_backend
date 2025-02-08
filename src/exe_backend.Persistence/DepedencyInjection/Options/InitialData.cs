using exe_backend.Contract.Common.Enums;

namespace exe_backend.Persistence.DepedencyInjection.Options;

public class InitialData
{
    public static IEnumerable<Domain.Models.Role> GetRoles()
    {
        return
        [
            Domain.Models.Role.Create(Guid.NewGuid(), RoleEnum.Admin.ToString()),
            Domain.Models.Role.Create(Guid.NewGuid(), RoleEnum.Member.ToString()),
        ];
    }
}
