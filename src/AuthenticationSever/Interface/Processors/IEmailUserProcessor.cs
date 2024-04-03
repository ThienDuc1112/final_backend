using IdentityServer4.Validation;
using Newtonsoft.Json.Linq;

namespace AuthenticationSever.Interface.Processors
{
    public interface IEmailUserProcessor
    {
        Task<GrantValidationResult> ProcessAsync(JObject userInfo, string email, string provider);
    }
}
