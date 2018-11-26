using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyCompany.MyProject.Domain;
using MyCompany.MyProject.Domain.DemoAggregate;

namespace MyCompany.MyProject.Persistence
{
    public class DefaultDbContext : DbContext
    {
        private readonly IDatetime _datetime;
        private readonly Settings _settings;

        public DefaultDbContext(IDatetime datetime, IOptions<Settings> options)
        {
            _datetime = datetime;
            _settings = options.Value;
        }

        public DbSet<Demo> Demo { get; set; }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionStrings.Default);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

        private void UpdateBasicInfo()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            Parallel.ForEach(entries, entry =>
            {
                if (entry.Entity is EntityBase auditableEntity)
                {
                    auditableEntity.UpdateBasicInfo(_datetime);
                }
            });
        }
    }
}
