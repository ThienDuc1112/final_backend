using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Entities
{
    public class ManageUser : IdentityUser
    {
        public string? FullName { get; set; }   
        public string? UrlAvatar { get; set; }
    }
}
