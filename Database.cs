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

    public static List<Production> GetProductions()
    {
        List<Production> productions = new();

        using var connection = new SqliteConnection("Data Source=production.db");

        connection.Open();

        string sql = @"
SELECT Id, Machine, Product, Quantity
FROM Productions;
";

        using var command = new SqliteCommand(sql, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Production production = new Production
            {
                Id = reader.GetInt32(0),
                Machine = reader.GetString(1),
                Product = reader.GetString(2),
                Quantity = reader.GetInt32(3)
            };

            productions.Add(production);
        }

        return productions;
    }

    public static Production? GetProductionById(int id)
    {
        using var connection = new SqliteConnection("Data Source=production.db");

        connection.Open();

        string sql = @"
SELECT Id, Machine, Product, Quantity
FROM Productions
WHERE Id = @id;
";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            Production production = new Production
            {
                Id = reader.GetInt32(0),
                Machine = reader.GetString(1),
                Product = reader.GetString(2),
                Quantity = reader.GetInt32(3)
            };

            return production;
        }

        return null;
    }

    public static void UpdateProduction(Production production)
    {
        using var connection = new SqliteConnection("Data Source=production.db");

        connection.Open();

        string sql = @"
UPDATE Productions
SET Machine = @machine,
    Product = @product,
    Quantity = @quantity
WHERE Id = @id;
";
        using var command = new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue("@machine", production.Machine);
        command.Parameters.AddWithValue("@product", production.Product);
        command.Parameters.AddWithValue("@quantity", production.Quantity);
        command.Parameters.AddWithValue("@id", production.Id);

        command.ExecuteNonQuery();
    }
}