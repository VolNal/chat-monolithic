using Dapper;
using Microsoft.Data.SqlClient;
using VolNal.Chat.Api.DAL.Factories.Interfaces;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;

namespace VolNal.Chat.Api.DAL.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory<SqlConnection> _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory<SqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<UserDto> GetAsync(string email)
    {
        var sql = @"SELECT * FROM items WHERE Email = @Email";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<UserDto>(sql, new {email});

        return result.FirstOrDefault();
    }
}