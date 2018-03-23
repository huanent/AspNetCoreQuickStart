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
        }
    }
}
