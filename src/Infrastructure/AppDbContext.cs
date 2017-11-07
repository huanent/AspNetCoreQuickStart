using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Demo> Demo { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
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
    }
}
