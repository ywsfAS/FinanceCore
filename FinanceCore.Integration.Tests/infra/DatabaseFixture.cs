using DbUp;
using FinanceCore.Database;

namespace FinanceCore.Integration.Tests.infra
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public string _connectionString { get; }
        public string _migrationPath { get; }
        public DatabaseFixture() {
            _connectionString = "Server=.;Database=FinanceCore_Test;Trusted_Connection=True;TrustServerCertificate=True"; 
            _migrationPath = "../migrations";
        }
        
        public Task InitializeAsync()
        {
            DropDatabase.For.SqlDatabase(_connectionString);
            DatabaseMigrator.MigrateFromFile(_connectionString , _migrationPath);
            return Task.CompletedTask;
        }
        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
   
}
