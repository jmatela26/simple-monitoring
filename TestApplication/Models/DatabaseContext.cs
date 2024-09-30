using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async Task SeedDataAsync()
        {
            if (!Accounts.Any() && !Monitors.Any() && !QueueGroups.Any())
            {
                var accountData = File.ReadAllText("Data/Account.json");
                var accounts = JsonConvert.DeserializeObject<List<Account>>(accountData);

                var monitorData = File.ReadAllText("Data/MonitorData.json");
                var monitors = JsonConvert.DeserializeObject<List<Monitor>>(monitorData);

                var queueGroupData = File.ReadAllText("Data/QueueGroup.json");
                var queueGroups = JsonConvert.DeserializeObject<List<QueueGroup>>(queueGroupData);

                Accounts.AddRange(accounts);
                QueueGroups.AddRange(queueGroups);
                Monitors.AddRange(monitors);

                await SaveChangesAsync();
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<QueueGroup> QueueGroups { get; set; }
    }
}
