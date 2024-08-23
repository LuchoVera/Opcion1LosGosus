using System.Data;
using Npgsql;

public sealed class DatabaseConnection
{
    private static readonly Lazy<DatabaseConnection> instance = 
        new Lazy<DatabaseConnection>(() => new DatabaseConnection());

    private readonly string connectionString;
    private IDbConnection connection;

    private DatabaseConnection()
    {
        connectionString = "Host=localhost;Port=5432;Database=gosus-db;Username=user;Password=password";
    }

    public static DatabaseConnection Instance
    {
        get
        {
            return instance.Value;
        }
    }

    public IDbConnection GetConnection()
    {
        if (connection == null || connection.State == ConnectionState.Closed)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }
        return connection;
    }
}
