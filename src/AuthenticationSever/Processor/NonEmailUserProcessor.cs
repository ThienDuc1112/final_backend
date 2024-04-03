using AuthenticationSever.Interface.Processors;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace AuthenticationSever.Processor
{
    public class NonEmailUserProcessor<TUser> : INonEmailUserProcessor where TUser : IdentityUser, new()
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<TUser> _userManager;
        public NonEmailUserProcessor(
            UserManager<TUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager;
        }
        public async Task<GrantValidationResult> ProcessAsync(JObject userInfo, string provider)
        {

            var userEmail = userInfo.Value<string>("email");

            if (provider.ToLower() == "linkedin")
                userEmail = userInfo.Value<string>("emailAddress");

            var userExternalId = userInfo.Value<string>("id");

            if (userEmail == null)
            {
                var existingUser = await _userManager.FindByLoginAsync(provider, userExternalId);
                if (existingUser == null)
                {
                    var customResponse = new Dictionary<string, object>();
                    customResponse.Add("userInfo", userInfo);
                    return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "could not retrieve user's email from the given provider, include email paramater and send request again.", customResponse);

                }
                else
                {
                    existingUser = await _userManager.FindByIdAsync(existingUser.Id);
                    var userClaims = await _userManager.GetClaimsAsync(existingUser);
                    return new GrantValidationResult(existingUser.Id, provider, userClaims, provider, null);
                }

            }
            else
            {
                var desiredRole = "CANDIDATE";
                var newUser = new TUser { Email = userEmail, UserName = userEmail };
                
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    await _userManager.AddLoginAsync(newUser, new UserLoginInfo(provider, userExternalId, provider));
                    if (!await _roleManager.RoleExistsAsync(desiredRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(desiredRole));
                    }
                    var roleResult = await _userManager.AddToRoleAsync(newUser, desiredRole);
                    if (!roleResult.Succeeded)
                    {
                        return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Failed to assign user role.");
                    }
                  
                    var userClaims = await _userManager.GetClaimsAsync(newUser);
                    return new GrantValidationResult(newUser.Id, provider, userClaims, provider, null);
                }
                return new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Your email was used, please try other");
            }

        }
    }
}
