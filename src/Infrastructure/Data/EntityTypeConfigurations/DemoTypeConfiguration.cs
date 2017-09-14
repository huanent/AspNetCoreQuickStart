using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityTypeConfigurations
{
    internal class DemoTypeConfiguration : IEntityTypeConfiguration<Demo>
    {
        public void Configure(EntityTypeBuilder<Demo> builder)
        {
            builder.HasKey(h => h.Id);
        }
    }
}
