using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Jsos3.Shared.Db;

public interface IDbConnectionFactory
{
    Task<IDbConnection> GetOpenStudentConnectionAsync();
    Task<IDbConnection> GetOpenAdministrationConnectionAsync();
    Task<IDbConnection> GetOpenLecturerConnectionAsync();
}

internal class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly IConfigurationRoot _configuration;

    public SqlConnectionFactory(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    private async Task<IDbConnection> GetOpenConnectionAsync(string connectionString)
    {
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public Task<IDbConnection> GetOpenStudentConnectionAsync() =>
            GetOpenConnectionAsync(_configuration.GetConnectionString("StudentSql"));

    public Task<IDbConnection> GetOpenAdministrationConnectionAsync() =>
        GetOpenConnectionAsync(_configuration.GetConnectionString("AdministrationSql"));

    public Task<IDbConnection> GetOpenLecturerConnectionAsync() =>
        GetOpenConnectionAsync(_configuration.GetConnectionString("LecturerSql"));
}
