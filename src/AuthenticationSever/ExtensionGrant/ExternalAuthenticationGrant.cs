using AuthenticationSever.Helper;
using AuthenticationSever.Interface;
using AuthenticationSever.Interface.Processors;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace AuthenticationSever.ExtensionGrant
{
    public class ExternalAuthenticationGrant<TUser> : IExtensionGrantValidator where TUser : IdentityUser, new()
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IProviderRepository _providerRepository;
        private readonly IGoogleAuthProvider _googleAuthProvider;
        private readonly INonEmailUserProcessor _nonEmailUserProcessor;
        private readonly IEmailUserProcessor _emailUserProcessor;

        public ExternalAuthenticationGrant(UserManager<TUser> userManager, IProviderRepository providerRepository,
            IGoogleAuthProvider googleAuthProvider,INonEmailUserProcessor nonEmailUserProcessor, IEmailUserProcessor emailUserProcessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
            _googleAuthProvider = googleAuthProvider ?? throw new ArgumentNullException(nameof(googleAuthProvider));
            _nonEmailUserProcessor = nonEmailUserProcessor ?? throw new ArgumentNullException(nameof(nonEmailUserProcessor));
            _emailUserProcessor = emailUserProcessor ?? throw new ArgumentNullException(nameof(nonEmailUserProcessor));
            _providers = new Dictionary<ProviderType, IExternalAuthProvider>
            {
                 {ProviderType.Google, _googleAuthProvider},
            };
            
        }

        private Dictionary<ProviderType, IExternalAuthProvider> _providers;

        public string GrantType => "external";



        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var provider = context.Request.Raw.Get("provider");
            if (string.IsNullOrWhiteSpace(provider))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
                return;
            }


            var token = context.Request.Raw.Get("external_token");
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid external token");
                return;
            }

            var requestEmail = context.Request.Raw.Get("email");

            var providerType = (ProviderType)Enum.Parse(typeof(ProviderType), provider, true);

            if (!Enum.IsDefined(typeof(ProviderType), providerType))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
                return;
            }

            //var userInfo =await _providers[providerType].GetUserInfoAsync(token);
            var userInfo = DecodeIdToken(token);

            if (userInfo == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "couldn't retrieve user info from specified provider, please make sure that access token is not expired.");
                return;
            }

            var externalId = userInfo.Value<string>("id");
            if (!string.IsNullOrWhiteSpace(externalId))
            {

                var user = await _userManager.FindByLoginAsync(provider, externalId);
                if (null != user)
                {
                    user = await _userManager.FindByIdAsync(user.Id);
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    context.Result = new GrantValidationResult(user.Id, provider, userClaims, provider, null);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(requestEmail))
            {
                context.Result = await _nonEmailUserProcessor.ProcessAsync(userInfo, provider);
                return;
            }

            context.Result = await _emailUserProcessor.ProcessAsync(userInfo, requestEmail, provider);
            return;
        }

        private JObject DecodeIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken);
            var tokenS = handler.ReadToken(idToken) as JwtSecurityToken;

            var payload = tokenS.Payload;
            var userProfile = new JObject
            {
                ["email"] = payload.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                ["name"] = payload.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                ["id"] = payload.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
            };

            return userProfile;
        }
    }
}
