class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        Database.InitializeDatabase();

        List<Production> productions = new List<Production>();

        while (running)
        {
            ShowMenu();

            string? input = Console.ReadLine();

            running = HandleMenuSelection(input, productions);
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("====================================");
        Console.WriteLine(" Factory Production Monitor");
        Console.WriteLine("====================================");
        Console.WriteLine();

        Console.WriteLine("1 - Show Production");
        Console.WriteLine("2 - Add Production");
        Console.WriteLine("3 - Update Production");
        Console.WriteLine("4 - Delete Production");
        Console.WriteLine("5 - Exit");
        Console.WriteLine();

        Console.Write("Selection: ");
    }

    static bool HandleMenuSelection(string? input, List<Production> productions)
    {
        switch (input)
        {
            case "1":
                ShowProduction(productions);
                return true;

            case "2":
                AddProduction(productions);
                return true;

            case "3":
                UpdateProduction(productions);
                return true;

            case "4":
                DeleteProduction(productions);
                return true;

            case "5":
                Console.WriteLine("Exiting...");
                return false;

            default:
                Console.WriteLine("Invalid selection.");
                return true;
        }
    }

    static void ShowProduction(List<Production> productions)
    {
        if (productions.Count == 0)
        {
            Console.WriteLine("No productions available.");
            return;
        }

        Console.WriteLine("Showing production...");
        foreach (Production production in productions)
        {
            Console.WriteLine(
            $"ID: {production.Id} | " +
            $"Machine: {production.Machine} | " +
            $"Product: {production.Product} | " +
            $"Quantity: {production.Quantity}");
        }
    }

    static void AddProduction(List<Production> productions)
    {
        Console.WriteLine("Adding production...");
        Console.Write("Machine: ");
        string? machine = Console.ReadLine();
        Console.Write("Product: ");
        string? product = Console.ReadLine();
        Console.Write("Quantity: ");
        int quantity = int.TryParse(Console.ReadLine(), out int parsedQuantity) ? parsedQuantity : 0;

        Production production = new Production
        {
            Id = productions.Count + 1,
            Machine = machine ?? "",
            Product = product ?? "",
            Quantity = quantity
        };

        productions.Add(production);
    }

    static Production? FindProductionById(List<Production> productions, int id)
    {
        foreach (Production production in productions)
        {
            if (production.Id == id)
            {
                return production;
            }
        }

        return null;
    }

    static void DeleteProduction(List<Production> productions)
    {
        if (productions.Count == 0)
        {
            Console.WriteLine("No productions available.");
            return;
        }

        Console.Write("Enter the ID of the production to delete: ");
        int id = int.TryParse(Console.ReadLine(), out int parsedId) ? parsedId : 0;

        Production? productionToRemove = FindProductionById(productions, id);

        if (productionToRemove != null)
        {
            productions.Remove(productionToRemove);
            Console.WriteLine($"Production with ID {id} deleted.");
        }
        else
        {
            Console.WriteLine($"Production ID {id} not found.");
        }
    }

    static void UpdateProduction(List<Production> productions)
    {
        if (productions.Count == 0)
        {
            Console.WriteLine("No productions available.");
            return;
        }

        Console.Write("Enter the ID of the production to update: ");
        int id = int.TryParse(Console.ReadLine(), out int parsedId) ? parsedId : 0;

        Production? productionToUpdate = FindProductionById(productions, id);

        if (productionToUpdate == null)
        {
            Console.WriteLine($"Production ID {id} not found.");
            return;
        }

        Console.Write("Machine: ");
        productionToUpdate.Machine = Console.ReadLine() ?? "";
        Console.Write("Product: ");
        productionToUpdate.Product = Console.ReadLine() ?? "";
        Console.Write("Quantity: ");
        productionToUpdate.Quantity = int.TryParse(Console.ReadLine(), out int parsedQuantity) ? parsedQuantity : 0;

        Console.WriteLine($"Production with ID {id} updated.");
    }
}
