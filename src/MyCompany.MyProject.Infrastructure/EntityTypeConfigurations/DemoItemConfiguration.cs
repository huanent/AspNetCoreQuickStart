using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.MyProject.Application.Entities.DemoAggregate;

namespace MyCompany.MyProject.Infrastructure.EntityTypeConfigurations
{
    public class DemoItemConfiguration : IEntityTypeConfiguration<DemoItem>
    {
        public void Configure(EntityTypeBuilder<DemoItem> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(32).IsUnicode().IsRequired();
        }
    }
}
