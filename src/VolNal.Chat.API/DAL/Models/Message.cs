namespace VolNal.Chat.Api.DAL.Models;
public class Message
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}