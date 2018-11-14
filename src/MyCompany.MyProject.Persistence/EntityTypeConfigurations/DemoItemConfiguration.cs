using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.MyProject.Domain.DemoAggregate;

namespace MyCompany.MyProject.Persistence.EntityTypeConfigurations
{
    public class DemoItemConfiguration : IEntityTypeConfiguration<DemoItem>
    {
        public void Configure(EntityTypeBuilder<DemoItem> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(32).IsUnicode().IsRequired();
        }
    }
}
