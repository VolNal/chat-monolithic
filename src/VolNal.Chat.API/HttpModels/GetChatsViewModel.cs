using VolNal.Chat.Api.Mapping.Interfaces;

namespace VolNal.Chat.API.HttpModels;

public class GetChatsViewModel : IGetChatsViewModel
{
    public string EmailCurrentUser { get; set; }
    public List<ChatViewModel> Chats { get; set; }
}