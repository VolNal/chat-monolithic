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
    
    public async Task<List<ChatDto>> GetAsync(UserDto user)
    {
        var sql = @"SELECT * FROM Chats WHERE CreatorId = @Id";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<ChatDto>(sql, new {user.Id});

        return result.ToList();
    }
    
    public async Task<ChatDto> CreateAsync(ChatDto chat)
    {
        var sql =
            "INSERT INTO Chats " +
            "OUTPUT INSERTED.* " +
            "VALUES(@Name, @Description, @Avatar, @CreatorId, @Type)";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<ChatDto>(sql, chat);

        return result.FirstOrDefault();
    }
}