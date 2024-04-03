using AuthenticationSever.DTOs;
using AuthenticationSever.Helper;
using AuthenticationSever.Interface;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

namespace AuthenticationSever.Providers
{
    public class GoogleAuthProvider<TUser> : IGoogleAuthProvider where TUser : IdentityUser, new()
    {

        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;
        public GoogleAuthProvider(

             IProviderRepository providerRepository,
             HttpClient httpClient
             )
        {

            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }
        public Provider Provider => _providerRepository.Get()
                                    .FirstOrDefault(x => x.Name.ToLower() == ProviderType.Google.ToString().ToLower());

        public async Task<JObject> GetUserInfoAsync(string accessToken)
        {
            var request = new Dictionary<string, string>
        {
            { "token", accessToken }
        };

            var result = await _httpClient.GetAsync(Provider.UserInfoEndPoint + QueryBuilder.GetQuery(request, ProviderType.Google));
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var result = await _httpClient.GetAsync(Provider.UserInfoEndPoint);
            if (result.IsSuccessStatusCode)
            {
                var infoObject = JObject.Parse(await result.Content.ReadAsStringAsync());
                return infoObject;
            }
            if (!result.IsSuccessStatusCode)
            {
                var statusCode = result.StatusCode;
                var errorContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Error StatusCode: {statusCode}, Content: {errorContent}");
                return null;
            }
            return null;
        }


    }
}
