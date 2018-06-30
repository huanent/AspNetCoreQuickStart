using MyCompany.MyProject.ApplicationCore;
using MyCompany.MyProject.ApplicationCore.Entities;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Demo> Demo { get; set; }

        readonly ISystemDateTime _systemDateTime;
        readonly IDbConnectionFactory _dbConnectionFactory;

        public AppDbContext(IDbConnectionFactory dbConnectionFactory, ISystemDateTime systemDateTime)
        {
            _systemDateTime = systemDateTime;
            _dbConnectionFactory = dbConnectionFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionFactory.Default());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
