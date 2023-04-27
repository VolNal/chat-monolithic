using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VolNal.Chat.Api.DAL.Factories.Interfaces;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;

namespace VolNal.Chat.Api.DAL.Repositories.Implementation;

public class MessageRepository : IMessageRepository
{
    private readonly IDbConnectionFactory<SqlConnection> _dbConnectionFactory;

    public MessageRepository(IDbConnectionFactory<SqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<MessageDto>> GetAsync(ChatDto chat)
    {
        var sql = @"SELECT TOP 50 * FROM Messages WHERE GroupId = @Id ORDER BY DESC";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<MessageDto>(sql, new { chat.Id });

        return result.ToList();
    }

    public async Task<MessageDto> CreateAsync(MessageDto message)
    {
        var sql = @"INSERT INTO Messages (ChatId, UserId, Content, Date)
                    VALUES (@ChatId, @UserId, @Content, @Date)";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<MessageDto>(sql, message);

        return result.FirstOrDefault();
    }
}