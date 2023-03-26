namespace VolNal.Chat.Api.DAL.Factories.Interfaces;

/// <summary>
/// Фабрика подключений к базе данных.
/// </summary>
public interface IDbConnectionFactory<TConnection> : IDisposable
{
    /// <summary>
    /// Создать подключение к БД.
    /// </summary>
    /// <returns></returns>
    Task<TConnection> CreateConnection();
}