namespace VolNal.Chat.Api.DAL.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] Avatar { get; set; }
    public string Password { get; set; }
}