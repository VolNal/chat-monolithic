using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtIdentity.Services;

public class AuthOptionsService
{
    public const string ISSUER = "ProjectJwtIdentity";
    public const string AUDIENCE = "ProjectJwtIdentity.com";
    private const string KEY = "0x7e57b0920210d400000";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new(Encoding.UTF8.GetBytes(KEY));
    }
}