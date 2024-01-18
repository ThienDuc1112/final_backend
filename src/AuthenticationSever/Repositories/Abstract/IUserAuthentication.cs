using AuthenticationSever.DTOs;

namespace AuthenticationSever.Repositories.Abstract
{
    public interface IUserAuthentication
    {
        public Task<Status> RegistrationAsync(RegisterDTO model);
        public Task TaskLogoutAsync();
    }
}
