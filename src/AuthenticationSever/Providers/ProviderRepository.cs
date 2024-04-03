using AuthenticationSever.DTOs;
using AuthenticationSever.Helper;
using AuthenticationSever.Interface;

namespace AuthenticationSever.Providers
{
    public class ProviderRepository : IProviderRepository
    {
        public IEnumerable<Provider> Get()
        {
            return ProviderDataSource.GetProviders();
        }
    }
}
