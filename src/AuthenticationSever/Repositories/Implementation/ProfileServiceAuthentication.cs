using AuthenticationSever.Entities;
using AuthenticationSever.Repositories.Abstract;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthenticationSever.Repositories.Implementation
{
    public class ProfileServiceAuthentication : IProfileService
    {
        private readonly UserManager<ManageUser> userManager;

        public ProfileServiceAuthentication(UserManager<ManageUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id),
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.Name, user.FullName),
            // Include any additional claims you want to include
        };

            // Add the role claim if requested
            if (context.RequestedClaimTypes.Contains(JwtClaimTypes.Role))
            {
                var roles = await userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ManageUser user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
