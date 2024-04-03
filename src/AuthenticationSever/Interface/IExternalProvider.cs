using Newtonsoft.Json.Linq;

namespace AuthenticationSever.Interface
{
    public interface IExternalAuthProvider
    {
        Task<JObject> GetUserInfoAsync(string accessToken);
    }
}
