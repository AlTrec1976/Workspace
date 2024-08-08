using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Workspace.DAL;

public abstract class BaseRepository
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    protected BaseRepository(ILogger<BaseRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task ExecuteAsync(string sql, object param)
    {
        try
        {
            using var connection = GetConnection();
            await connection.QueryAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка ExecuteAsync");
            throw;
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
    {
        try
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<T>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка QueryAsync");
            throw;
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object param = null)
    {
        try
        {
            using var connection = GetConnection();
            return await connection.QueryFirstAsync<T>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка QuerySingleAsync");
            throw;
        }
    }

    protected NpgsqlConnection GetConnection()
    {
        try
        {
            return new NpgsqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка соединения в BaseRepository");
            throw;
        }
    }
}
