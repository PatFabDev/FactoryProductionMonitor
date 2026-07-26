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

    public static void AddProduction(Production production)
    {
        using var connection = new SqliteConnection("Data Source=production.db");

        connection.Open();

        string sql = @"
INSERT INTO Productions (Machine, Product, Quantity)
VALUES (@machine, @product, @quantity);
";

        using var command = new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue("@machine", production.Machine);
        command.Parameters.AddWithValue("@product", production.Product);
        command.Parameters.AddWithValue("@quantity", production.Quantity);

        command.ExecuteNonQuery();
    }
}