namespace AuthenticationSever.Configuration
{
    using IdentityModel;
    using IdentityServer4.Models;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource(
                    "roles",
                    "User role(s)",
                    new List<string>() { "role" })
          };

        }

        //public static IEnumerable<ApiResource> GetApiResources()
        //{
        //    return new List<ApiResource>
        //{
        //    new ApiResource("providerAPI", "Provider API"),
        //    new ApiResource("api1", "Job API"),
        //    new ApiResource("candidateAPI", "Candidate API")
        //};
        //}

        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("providerAPI", "Provider API")
            {
                UserClaims = {  JwtClaimTypes.Role }

            },
            new ApiScope("jobAPI", "Job API"),
            new ApiScope("candidateAPI", "Candidate API")
        };

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "jobAPI", "openid", }
            },
            new Client
            {
                ClientId = "nextjs",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret("adminSecret".Sha256()) },
                AllowedScopes = { "providerAPI", "jobAPI", "candidateAPI", "openid", "profile", "email", "roles","offline_access" },
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true,
                RedirectUris = { "http://localhost:3000/Home" },
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins= { "http://localhost:3000" },
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                //AbsoluteRefreshTokenLifetime = 360000,
                SlidingRefreshTokenLifetime = 360000,

            },

        };
        }
    }
}