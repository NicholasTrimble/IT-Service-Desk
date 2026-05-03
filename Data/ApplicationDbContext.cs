using IT_Service_Desk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IT_Service_Desk.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get pending changes for the WorkOrder entity
            var entries = ChangeTracker.Entries<WorkOrder>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            // Loop through each change and generate a log
            foreach (var entry in entries)
            {
                var log = new AuditLog
                {
                    WorkOrderId = entry.Entity.Id,
                    Action = entry.State == EntityState.Added ? "Created" : "Updated",
                    ChangedBy = "System",
                    Timestamp = DateTime.Now
                };

                AuditLogs.Add(log);
            }

            // Save
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}