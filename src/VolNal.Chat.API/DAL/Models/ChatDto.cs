namespace VolNal.Chat.Api.DAL.Models;

public class ChatDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Avatar { get; set; }
    public int CreatorId { get; set; }
    public Type Type { get; set; }
}

public enum Type
{
    Private,
    Group
}