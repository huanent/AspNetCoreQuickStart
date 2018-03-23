using ApplicationCore.Entities;
using ApplicationCore.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Demo> Demo { get; set; }

        readonly ISystemDateTime _systemDateTime;

        public AppDbContext(DbContextOptions options, ISystemDateTime systemDateTime) : base(options)
        {
            _systemDateTime = systemDateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RegisterEntityTypeConfigurations(modelBuilder);
        }

        private static void RegisterEntityTypeConfigurations(ModelBuilder modelBuilder)
        {
            string typeName = typeof(IEntityTypeConfiguration<>).Name;
            var assembly = Assembly.GetExecutingAssembly();

            assembly.GetTypes()
                    .Where(w => w.GetTypeInfo().ImplementedInterfaces.Any(a => a.Name == typeName))
                    .ToList()
                    .ForEach(item =>
                    {
                        dynamic instance = item.FullName;
                        modelBuilder.ApplyConfiguration(assembly.CreateInstance(instance));
                    });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateBasicInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateBasicInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateBasicInfo()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.Entity is EntityBase auditableEntity)
                {
                    auditableEntity.UpdateBasicInfo(_systemDateTime);
                }
            }
        }
    }
}
