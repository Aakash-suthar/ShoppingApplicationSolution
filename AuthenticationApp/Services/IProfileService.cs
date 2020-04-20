using AuthenticationApp.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationApp.Services
{
    public class ProfileService : IProfileService
    {
        UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
            new Claim(IdentityServerConstants.StandardScopes.Email, user.Email),
            //new Claim("uid", user.Id),
            //new Claim(JwtClaimTypes.ZoneInfo, user.TimeZone)
        };
/*            if (user.UserType != null) claims.Add(new Claim("mut", ((int)user.UserType).ToString()));*/
            context.IssuedClaims.AddRange(claims);
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            context.IsActive = user != null;
            return Task.FromResult(0);
        }
    }
}
