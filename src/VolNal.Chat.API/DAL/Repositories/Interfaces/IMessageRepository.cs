using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.Api.DAL.Models;

namespace VolNal.Chat.Api.DAL.Repositories.Interfaces;

public interface IMessageRepository
{
    public Task<List<MessageDto>> GetAsync(ChatDto chat);
    public Task<MessageDto> CreateAsync(MessageDto message);
}