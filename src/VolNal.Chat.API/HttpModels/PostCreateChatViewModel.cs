using VolNal.Chat.Api.Mapping.Interfaces;

namespace VolNal.Chat.API.HttpModels;

public class PostCreateChatViewModel : IPostCreateChatViewModel
{
    public string EmailCurrentUser { get; set; }
    public string Name { get; set; }
}