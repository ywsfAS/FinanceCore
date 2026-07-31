using DbUp;

var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__FinanceCore")
    ?? throw new InvalidOperationException(
        "ConnectionStrings__FinanceCore is not configured.");
EnsureDatabase.For.SqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem("migrations")
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();
if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();

    Environment.Exit(-1);
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Database upgraded successfully.");
Console.ResetColor();
