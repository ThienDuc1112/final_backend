using AuthenticationSever.DTOs;
using AuthenticationSever.Entities;
using AuthenticationSever.Repositories.Abstract;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationSever.Repositories.Implementation
{
    public class UserAuthentication: IUserAuthentication
    {
        private readonly UserManager<ManageUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ManageUser> signInManager;

        public UserAuthentication(UserManager<ManageUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ManageUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public async Task<Status> RegistrationAsync(RegisterDTO model)
        {
            var status = new Status();
            if(model.Email == null)
            {
                status.StatusCode = 0;
                status.Message = "Please provide an email address";
                return status;
            }
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "This email already exists";
                return status;
            }
            var appUser = new ManageUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            };
            appUser.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.Role,
                ClaimValue = model.Role
            });

            var result = await userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;


                foreach (var item in result.Errors)
                {
                    status.Message += item.Description + " ";
                }
            }
            else
            {
                if (!await roleManager.RoleExistsAsync(model.Role))
                {
                    await roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                if (await roleManager.RoleExistsAsync(model.Role))
                {
                    await userManager.AddToRoleAsync(appUser, model.Role);
                }
                status.StatusCode = 1;
                status.Message = appUser.Id;
            }
            return status;
        }

        public Task TaskLogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
