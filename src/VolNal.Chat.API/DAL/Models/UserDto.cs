using JwtIdentity.Models.Interfaces;

namespace VolNal.Chat.Api.DAL.Models;

public class UserDto:IIdentityUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] Avatar { get; set; }
    public string Password { get; set; }
}