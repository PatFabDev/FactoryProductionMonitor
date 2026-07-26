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
                ShowProduction();
                return true;

            case "2":
                AddProduction();
                return true;

            case "3":
                UpdateProduction();
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

    static void ShowProduction()
    {
        List<Production> productions = Database.GetProductions();

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

    static void AddProduction()
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
            Machine = machine ?? "",
            Product = product ?? "",
            Quantity = quantity
        };

        Database.AddProduction(production);
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

    static void UpdateProduction()
    {
        Console.Write("Enter the ID of the production to update: ");
        int id = int.TryParse(Console.ReadLine(), out int parsedId) ? parsedId : 0;

        Production? production = Database.GetProductionById(id);

        if (production == null)
        {
            Console.WriteLine("Production not found.");
            return;
        }

        Console.WriteLine($"Current machine : {production.Machine}");
        Console.WriteLine($"Current product : {production.Product}");
        Console.WriteLine($"Current quantity: {production.Quantity}");

        Console.Write($"Machine ({production.Machine}): ");
        string? machine = Console.ReadLine();

        production.Machine = string.IsNullOrWhiteSpace(machine)
            ? production.Machine
            : machine;

        Console.Write($"Product ({production.Product}): ");
        string? product = Console.ReadLine();

        production.Product = string.IsNullOrWhiteSpace(product)
            ? production.Product
            : product;

        Console.Write($"Quantity ({production.Quantity}): ");
        string? quantityInput = Console.ReadLine();

        production.Quantity = int.TryParse(quantityInput, out int parsedQuantity)
            ? parsedQuantity
            : production.Quantity;

        Database.UpdateProduction(production);
    }
}
