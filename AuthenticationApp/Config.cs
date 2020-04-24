// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthenticationApp
{
    public static class Config
    {
        /*    public static IEnumerable<IdentityResource> Ids =>
                new IdentityResource[]
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    customProfile  = new IdentityResource(
                        name: "other",
                        displayName: "other2",
                       claimTypes : new [] {
                             JwtClaimTypes.PhoneNumber,
                            JwtClaimTypes.Email
                            }
                        )


                };*/

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                name: "custom",
                displayName: "Custom",
                claimTypes: new[] { 
                JwtClaimTypes.PhoneNumber,
                JwtClaimTypes.Email
                });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
              new IdentityResources.Email(),
            };
        }


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("productapi", "Product"),
                new ApiResource("orderapi", "Order"),
                new ApiResource("paymentapi", "Payment"),
                new ApiResource("shipmentapi", "Shipment"),
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = { "productapi", "orderapi", "paymentapi", "shipmentapi", "customProfile" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "code",
                    ClientName = "Shopping App",

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "https://localhost:44393/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44393/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44393/signout-callback-oidc" },
                     AllowAccessTokensViaBrowser = true,
                     AlwaysSendClientClaims = true,
                     AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "orderapi","paymentapi","shipmentapi","productapi" }
                },


                 new Client
                {
                    ClientId = "shipmentagent",
                    ClientName = "Shipment Agent",

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets = { new Secret("shipment".Sha256()) },

                    RedirectUris = { "https://localhost:44314/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44314/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44314/signout-callback-oidc" },
                     AllowAccessTokensViaBrowser = true,
                     AlwaysSendClientClaims = true,
                     AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "orderapi","paymentapi","shipmentapi","productapi" }
                },


                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };
    }
}