using VolNal.Chat.Api.DAL.Models;

namespace VolNal.Chat.Api.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<UserDto> GetAsync(UserDto user);
    public Task<UserDto> CreateAsync(UserDto user);
}