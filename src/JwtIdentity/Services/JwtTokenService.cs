using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtIdentity.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace JwtIdentity.Services;

public class JwtTokenService
{
    public string GetToken(IIdentityUser identityUser)
    {
        var claims = new List<Claim>
            {new(ClaimTypes.Email, identityUser.Email)};
        var jwt = new JwtSecurityToken(
            AuthOptionsService.ISSUER,
            AuthOptionsService.AUDIENCE,
            claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия 1 день
            signingCredentials: new SigningCredentials(AuthOptionsService.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }
}