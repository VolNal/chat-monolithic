using VolNal.Chat.Api.Mapping.Interfaces;
using Type = VolNal.Chat.Api.DAL.Models.Type;

namespace VolNal.Chat.API.HttpModels;

public class PostCreateChatViewModel : IPostCreateChatViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[]? Avatar { get; set; }
    public Type Type { get; set; }
}