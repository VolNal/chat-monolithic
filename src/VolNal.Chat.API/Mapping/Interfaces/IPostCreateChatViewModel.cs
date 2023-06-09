namespace VolNal.Chat.Api.Mapping.Interfaces;
using Type = VolNal.Chat.Api.DAL.Models.Type;

public interface IPostCreateChatViewModel
{
    public string Name { get; set; }
    public Type Type { get; set; }
}