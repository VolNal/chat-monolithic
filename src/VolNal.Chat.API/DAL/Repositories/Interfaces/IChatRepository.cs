using VolNal.Chat.Api.DAL.Models;

namespace VolNal.Chat.Api.DAL.Repositories.Interfaces;

public interface IChatRepository
{
    public Task<List<ChatDto>> GetAsync(UserDto user);
    public Task<ChatDto> CreateAsync(ChatDto chat);
}