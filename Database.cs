using Microsoft.Data.Sqlite;

public static class Database
{
    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection("Data Source=production.db");

        connection.Open();

        string sql = @"
CREATE TABLE IF NOT EXISTS Productions (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Machine TEXT NOT NULL,
    Product TEXT NOT NULL,
    Quantity INTEGER NOT NULL
);";

        using var command = new SqliteCommand(sql, connection);

        command.ExecuteNonQuery();
    }
}