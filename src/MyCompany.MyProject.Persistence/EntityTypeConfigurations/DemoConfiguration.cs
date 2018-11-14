using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.MyProject.Domain.DemoAggregate;

namespace MyCompany.MyProject.Persistence.EntityTypeConfigurations
{
    public class DemoConfiguration : IEntityTypeConfiguration<Demo>
    {
        public void Configure(EntityTypeBuilder<Demo> builder)
        {
            builder.InitBuildInProperty();
            builder.Property(p => p.Name).IsRequired().IsUnicode().HasMaxLength(16);
            builder.HasMany(h => h.DemoItems).WithOne().HasForeignKey(h => h.DemoId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
