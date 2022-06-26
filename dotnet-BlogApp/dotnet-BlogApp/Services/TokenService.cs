using dotnet_BlogApp.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_BlogApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            // Claims to put in Jwt
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email)
            };

            // Added for Microsoft Identity Roles
            IList<string>? roles = await _userManager.GetRolesAsync(appUser);

            if (roles != null)
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Security Algorithm for Signing Credentials
            SigningCredentials? creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Actual Jwt body creation
            SecurityTokenDescriptor? tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            // Token Creator
            JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();

            // Token Creation
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            // Token conversion to string
            return tokenHandler.WriteToken(token);
        }
    }
}
