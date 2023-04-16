using Dapper;
using Microsoft.Data.SqlClient;
using VolNal.Chat.API.Controllers;
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

    public async Task<UserDto> GetAsync(UserDto user)
    {
        var sql = @"SELECT * FROM Users WHERE Email = @Email";

        var connection = await _dbConnectionFactory.CreateConnection();
        var result = await connection.QueryAsync<UserDto>(sql, new {user.Email});

        return result.FirstOrDefault();
    }

    public async Task CreateAsync(AuthorizeUserViewModel model)
    {
        throw new NotImplementedException();
    }
}