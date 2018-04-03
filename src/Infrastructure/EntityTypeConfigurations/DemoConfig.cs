using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.EntityTypeConfigurations
{
    public class DemoConfig : IEntityTypeConfiguration<Demo>
    {
        public void Configure(EntityTypeBuilder<Demo> builder)
        {
            builder.InitBuildInProperty();
            builder.Property(p => p.Name).IsRequired().IsUnicode().HasMaxLength(16);
            builder.Property(p => p.Age).IsRequired();
        }
    }
}
