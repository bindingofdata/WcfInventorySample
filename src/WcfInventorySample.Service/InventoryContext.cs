using System.Data.Entity;

namespace WcfInventorySample.Service
{
    /// <summary>
    /// EF6 DbContext for the inventory database. The connection string named
    /// "InventoryContext" is read from the host application's config file
    /// (WcfInventorySample.Host App.config) — class libraries do not supply
    /// their own connection string at runtime.
    /// </summary>
    public class InventoryContext : DbContext
    {
        static InventoryContext()
        {
            // Create the database on first use if it does not exist yet.
            Database.SetInitializer(new CreateDatabaseIfNotExists<InventoryContext>());
        }

        public InventoryContext()
            : base("name=InventoryContext")
        {
            // Entities never leave the service (clients get DTOs), so there
            // is no reason to pay for change-tracking proxies or lazy loading.
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<StockTransaction> StockTransactions { get; set; }
    }
}
