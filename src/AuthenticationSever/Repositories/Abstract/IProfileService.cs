using IdentityServer4.Models;

namespace AuthenticationSever.Repositories.Abstract
{
    public interface IProfileService
    {
        Task GetProfileDataAsync(ProfileDataRequestContext context);
        Task IsActiveAsync(IsActiveContext context);
    }
}
