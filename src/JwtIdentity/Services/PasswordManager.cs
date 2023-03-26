using JwtIdentity.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JwtIdentity.Services;

public class PasswordManager
{
    private readonly IPasswordHasher<IIdentityUser> _passwordHasher;

    public PasswordManager(IPasswordHasher<IIdentityUser> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
    }
}