using AuthenticationSever.DTOs;

namespace AuthenticationSever.Helper
{
    public class ProviderDataSource
    {
        public static IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>
            {
                new Provider
                {
                    ProviderId = 1,
                    Name = "Google",
                    UserInfoEndPoint = "https://www.googleapis.com/oauth2/v3/userinfo"
                }           
            };
        }
    }
}
