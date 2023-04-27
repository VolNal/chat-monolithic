using Dapper;
using Microsoft.Data.SqlClient;
using VolNal.Chat.Api.DAL.Factories.Interfaces;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;

namespace VolNal.Chat.Api.DAL.Repositories.Implementation;

public class ChatRepository : IChatRepository
{
    private readonly IDbConnectionFactory<SqlConnection> _dbConnectionFactory;

    public ChatRepository(IDbConnectionFactory<SqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<ChatDto> CreateAsync(ChatDto chat)
    {
        var sql = @"INSERT INTO Chats (Name, Description, Avatar, CreatorId, Type) 
                    VALUES (@Name, @Description, @Avatar, @CreatorId, @Type)";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<ChatDto>(sql, chat);

        return result.FirstOrDefault();
    }

    public Task<List<ChatDto>> GetAsync(UserDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ChatDto>> GetAllAsync(UserDto user)
    {
        var sql = @"SELECT * FROM Chats WHERE Email = @Email";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<ChatDto>(sql, new { user.Email });

        return result.ToList();
    }
}