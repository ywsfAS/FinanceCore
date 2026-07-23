using DbUp;
namespace FinanceCore.Database
{
    public class DatabaseMigrationException : Exception
    {
         public DatabaseMigrationException(string message) : base(message) { }
    }
    public static class DatabaseMigrator
    {
        public static void MigrateFromFile(string connectionString ,string path)
        {

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem(path)
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new DatabaseMigrationException($"Migration Failed : {result.Error.Message}");
            }
        }
    }
}
