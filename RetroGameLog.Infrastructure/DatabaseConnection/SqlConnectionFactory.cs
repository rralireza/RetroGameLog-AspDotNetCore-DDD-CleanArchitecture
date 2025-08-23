using Microsoft.Data.SqlClient;
using RetroGameLog.Application.Abstractions.Data;
using System.Data;

namespace RetroGameLog.Infrastructure.DatabaseConnection;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);

        connection.Open();

        return connection;
    }
}
