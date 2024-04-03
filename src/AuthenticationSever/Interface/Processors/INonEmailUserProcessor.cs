using IdentityServer4.Validation;
using Newtonsoft.Json.Linq;

namespace AuthenticationSever.Interface.Processors
{
    public interface INonEmailUserProcessor
    {
        Task<GrantValidationResult> ProcessAsync(JObject userInfo, string provider);
    }
}
