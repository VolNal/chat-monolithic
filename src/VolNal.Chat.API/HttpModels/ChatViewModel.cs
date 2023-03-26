using VolNal.Chat.Api.Mapping.Interfaces;

namespace VolNal.Chat.API.HttpModels;

public class ChatViewModel : IChatViewModel
{
    public string Name { get; set; }
    public byte[] Avatar { get; set; }
    public string LastMessage { get; set; }
}