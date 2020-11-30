using IdentityServer4.Models;
using System.Collections.Generic;

namespace AoTTG2.IDS
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(), 
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "AoTTG2",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://127.0.0.1:51772/" },
                    AllowedScopes = { "openid", "profile" },
                    PostLogoutRedirectUris = { "http://127.0.0.1:51772/" },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                },

                new Client
                {
                    ClientId = "Photon",
                    ClientSecrets =  { new Secret("abc".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RedirectUris = { "https://127.0.0.1:5055/" },
                    AllowedScopes = {"openid", "profile"}

                }
            };
    }
}