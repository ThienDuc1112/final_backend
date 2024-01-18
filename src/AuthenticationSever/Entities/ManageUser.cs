using Microsoft.AspNetCore.Identity;

namespace AuthenticationSever.Entities
{
    public class ManageUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? UrlAvatar { get; set; }
        public ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();
    }
}
