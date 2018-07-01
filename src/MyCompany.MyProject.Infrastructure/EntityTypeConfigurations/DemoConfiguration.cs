using MyCompany.MyProject.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace MyCompany.MyProject.Infrastructure.EntityTypeConfigurations
{
    public class DemoConfiguration : IEntityTypeConfiguration<Demo>
    {
        public void Configure(EntityTypeBuilder<Demo> builder)
        {
            builder.InitBuildInProperty();
            builder.Property(p => p.Name).IsRequired().IsUnicode().HasMaxLength(16);
            builder.Property(p => p.Age).IsRequired();
        }
    }
}
