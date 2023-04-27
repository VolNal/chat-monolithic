using Microsoft.AspNetCore.SignalR;

namespace VolNal.Chat.Api.Services;

public class ChatHub : Hub
{
    public async Task JoinChat(string chatName, string userName)
    {
        await OnConnectedAsync(chatName);
        await SendMessageToChatAsync(chatName, $"{userName} вступил в группу.");
    }

    public async Task OnConnectedAsync(string chatName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
    }

    public async Task OnDisconnectedGroup(string chatName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
    }

    [HubMethodName("privateMethod")]
    public async Task SendMessageToChatAsync(string chatName, string message)
    {
        await Clients.Group(chatName).SendAsync("PostMessage", message);
    }
}