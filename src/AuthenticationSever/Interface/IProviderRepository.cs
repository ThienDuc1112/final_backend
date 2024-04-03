using AuthenticationSever.DTOs;

namespace AuthenticationSever.Interface
{
    public interface IProviderRepository
    {
        IEnumerable<Provider> Get();
    }
}
